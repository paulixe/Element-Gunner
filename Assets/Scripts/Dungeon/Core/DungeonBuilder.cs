using PII.Dungeon.DungeonGraph;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;
namespace PII.Dungeon
{
    /// <summary>
    /// Creates a dungeon from room templates and dungeon graphs
    /// </summary>
    public class DungeonBuilder : MonoBehaviour
    {
        private Dictionary<RoomNode, Room> RoomPlaced = new Dictionary<RoomNode, Room>();
        List<RoomTemplateSO> roomTemplates;
        const int MAX_DUNGEON_BUILD_ATTEMPT = 1000;
        bool buildIterationFail = false;

        public List<InstantiatedRoom> GenerateDungeon(List<RoomTemplateSO> roomTemplates, List<DungeonGraphSO> dungeonGraphs)
        {
            this.roomTemplates = roomTemplates;

            int iterations = 0;

            //Debug.Log("begin generating dungeon");
            do
            {
                //Debug.Log("tryBuildDungeon: " + iterations);
                DungeonGraphSO dungeonGraph = dungeonGraphs[Random.Range(0, dungeonGraphs.Count)];
                TryBuildDungeon(dungeonGraph);
            }
            while (!IsDungeonValid() && iterations++ < MAX_DUNGEON_BUILD_ATTEMPT);


            if (iterations == MAX_DUNGEON_BUILD_ATTEMPT)
                Debug.Log("Didn't manage to build the dungeon");
            return InstantiateRooms(RoomPlaced.Values.ToList());
        }
        private List<InstantiatedRoom> InstantiateRooms(List<Room> rooms)
        {
            List<InstantiatedRoom> newRooms = new List<InstantiatedRoom>();

            foreach (Room room in rooms)
            {
                Vector2Int gridPosition = room.lowerBounds - room.RoomTemplate.lowerBounds;
                Vector3 position = new(gridPosition.x, gridPosition.y, 0);
                GameObject child = Instantiate(room.RoomTemplate.prefab, position, Quaternion.identity, transform);
                InstantiatedRoom instantiatedRoom = child.GetComponent<InstantiatedRoom>();
                instantiatedRoom.Initialize(room);


                newRooms.Add(instantiatedRoom);
            }
            return newRooms;
        }
        private void TryBuildDungeon(DungeonGraphSO dungeonGraph)
        {
            buildIterationFail = false;
            RoomPlaced.Clear();
            Queue<RoomNode> queue = new Queue<RoomNode>();

            //Place Entrance
            RoomNode firstRoomNode = dungeonGraph.GetEntrance();
            queue.Enqueue(firstRoomNode);




            int roomIterations = 0;
            while (!buildIterationFail && queue.TryDequeue(out RoomNode roomNode) && roomIterations++ < dungeonGraph.ChildrenCount)
            {
                foreach (RoomNode child in roomNode.children)
                    queue.Enqueue(child);

                Room room = CreateRoomFor(roomNode);
            }

        }

        private bool HasOverlap(List<Room> rooms)
        {
            Queue<Room> roomsToCheck = new Queue<Room>(rooms);
            while (roomsToCheck.TryDequeue(out Room room1))
                foreach (Room room2 in rooms)
                    if (room1 != room2 && room2.OverlapWith(room1))
                        return true;
            return false;
        }
        private bool IsDungeonValid()
            => RoomPlaced.Count > 0 && !HasOverlap(RoomPlaced.Values.ToList());

        private Room CreateRoomFor(RoomNode roomNode)
        {
            RoomTemplateSO roomTemplate = GetRoomTemplateFor(roomNode);
            Room room = new Room(roomTemplate);
            RoomPlaced.Add(roomNode, room);

            if (roomNode.roomType == RoomType.Entrance)
            {
                room.lowerBounds = room.RoomTemplate.lowerBounds;
                room.upperBounds = room.RoomTemplate.upperBounds;
            }
            else
            {
                Room parent = RoomPlaced[roomNode.parents[0]];
                if (!TryPositioningRoom(room, parent))
                    buildIterationFail = true;
            }

            return room;
        }
        private bool TryPositioningRoom(Room roomToPlace, Room roomToConnectTo)
        {
            List<Doorway> validParentDoorways = GetValidDoorways(roomToConnectTo);
            if (validParentDoorways.Count == 0)
                return false;
            Doorway doorwayToConnectTo = validParentDoorways[Random.Range(0, validParentDoorways.Count)];
            if (doorwayToConnectTo == null)
                return false;
            Doorway oppositeDoorway = GetOppositeDoorway(doorwayToConnectTo.orientation, roomToPlace.Doorways);
            if (oppositeDoorway == null)
                return false;


            PositionRoom(roomToConnectTo, roomToPlace, doorwayToConnectTo, oppositeDoorway);
            oppositeDoorway.isConnected = true;
            doorwayToConnectTo.isConnected = true;
            return true;
        }
        private void PositionRoom(Room parentRoom, Room childRoom, Doorway parentDoorway, Doorway childDoorway)
        {
            Vector2Int parentDoorwayPosition =
                parentRoom.lowerBounds + parentDoorway.position - parentRoom.RoomTemplate.lowerBounds;
            Vector2Int adjustment = Vector2Int.zero;
            switch (parentDoorway.orientation)
            {
                case Orientation.north:
                    adjustment = new(0, 1);
                    break;
                case Orientation.south:
                    adjustment = new(0, -1);
                    break;
                case Orientation.west:
                    adjustment = new(-1, 0);
                    break;
                case Orientation.east:
                    adjustment = new(1, 0);
                    break;
            }
            childRoom.lowerBounds = parentDoorwayPosition + adjustment + childRoom.RoomTemplate.lowerBounds - childDoorway.position;
            childRoom.upperBounds = childRoom.lowerBounds + childRoom.RoomTemplate.upperBounds - childRoom.RoomTemplate.lowerBounds;
        }
        private Doorway GetOppositeDoorway(Orientation oppositeOriention, List<Doorway> doorwaysToChoose)
        {
            foreach (Doorway doorway in doorwaysToChoose)
            {
                if (doorway.orientation == Orientation.north && oppositeOriention == Orientation.south)
                    return doorway;
                else if (doorway.orientation == Orientation.south && oppositeOriention == Orientation.north)
                    return doorway;
                else if (doorway.orientation == Orientation.west && oppositeOriention == Orientation.east)
                    return doorway;
                else if (doorway.orientation == Orientation.east && oppositeOriention == Orientation.west)
                    return doorway;
            }
            return null;
        }
        private List<Doorway> GetValidDoorways(Room room)
        {
            List<Doorway> validDoorways = new List<Doorway>();
            foreach (Doorway doorway in room.Doorways)
                if (!doorway.isConnected & !doorway.isUnavailable)
                    validDoorways.Add(doorway);
            return validDoorways;
        }

        private RoomTemplateSO GetRoomTemplateFor(RoomNode roomNode) =>
            GetRandomRoomTemplate(roomNode.roomType);

        private RoomTemplateSO GetRandomRoomTemplate(RoomType roomType)
        {
            List<RoomTemplateSO> roomTemplatesOfTypeRoomType = new List<RoomTemplateSO>();
            foreach (RoomTemplateSO roomTemplate in roomTemplates)
                if (roomTemplate.RoomType.GetHashCode() == roomType.GetHashCode())
                    roomTemplatesOfTypeRoomType.Add(roomTemplate);
            return (roomTemplatesOfTypeRoomType[Random.Range(0, roomTemplatesOfTypeRoomType.Count)]);
        }


    }
}