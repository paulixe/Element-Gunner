using System.Collections.Generic;
using UnityEngine;
using static Utilities.CommonUtilities;
namespace PII.Dungeon
{
    /// <summary>
    /// room:
    ///     - Used for creating the dungeon
    ///     - Wrapper for some data used in instantiatedRoom
    /// </summary>
    public class Room
    {
        public RoomTemplateSO RoomTemplate;
        public Vector2Int lowerBounds;
        public Vector2Int upperBounds;

        public RoomType RoomType => RoomTemplate.RoomType;
        public List<Doorway> Doorways = new List<Doorway>();
        public Room(RoomTemplateSO roomTemplate)
        {
            RoomTemplate = roomTemplate;
            foreach (Doorway doorway in roomTemplate.doorwayList)
                Doorways.Add(new Doorway(doorway));
        }
        public bool IsCorridor => RoomType == RoomType.CorridorEW || RoomType == RoomType.CorridorNS;
        public bool OverlapWith(Room anotherRoom)
        {
            bool IsOverlappingX =
                IsOverlapping(lowerBounds.x, upperBounds.x, anotherRoom.lowerBounds.x, anotherRoom.upperBounds.x);
            bool IsOverlappingY =
                IsOverlapping(lowerBounds.y, upperBounds.y, anotherRoom.lowerBounds.y, anotherRoom.upperBounds.y);
            return IsOverlappingX && IsOverlappingY;
        }
    }
}