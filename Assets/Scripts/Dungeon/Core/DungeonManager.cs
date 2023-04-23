using PII.Dungeon.DungeonGraph;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
namespace PII.Dungeon
{
    /// <summary>
    /// Reference the current room we are in and every other rooms
    /// Assign events
    /// Use the dungeon builder to create a dungeon
    /// </summary>
    [RequireComponent(typeof(DungeonBuilder))]
    public class DungeonManager : SingletonMonoBehaviour<DungeonManager>
    {
        InstantiatedRoom currentRoom;
        DungeonBuilder dungeonBuilder;
        List<InstantiatedRoom> rooms = new List<InstantiatedRoom>();

        protected override void Awake()
        {
            base.Awake();
            dungeonBuilder = GetComponent<DungeonBuilder>();
        }
        private void OnEnable()
        {
            StaticEventHandler.OnRoomChange += OnRoomChange;
            StaticEventHandler.OnRoomCleared += OnRoomCleared;
        }
        private void OnDisable()
        {
            StaticEventHandler.OnRoomChange -= OnRoomChange;
            StaticEventHandler.OnRoomCleared -= OnRoomCleared;
        }
        private void OnRoomCleared(RoomClearedEventArgs roomClearedEventArgs)
        {
            currentRoom.OpenDoors();
        }
        private void OnRoomChange(RoomChangedEventArgs roomChangedEventArgs)
        {
            currentRoom = roomChangedEventArgs.CurrentRoom;
        }
        public void ChangeDungeon(List<RoomTemplateSO> roomTemplates, List<DungeonGraphSO> dungeonGraphs)
            => ChangeRooms(dungeonBuilder.GenerateDungeon(roomTemplates, dungeonGraphs));

        public void ChangeRooms(List<InstantiatedRoom> newRooms)
        {
            foreach (InstantiatedRoom room in rooms)
                Destroy(room.gameObject);
            rooms.Clear();
            rooms = newRooms;
            currentRoom = rooms.Find(r => r.Room.RoomType == RoomType.Entrance);
            StaticEventHandler.ChangeRoom(currentRoom);
            currentRoom.HasBeenVisited = true;
        }
        public InstantiatedRoom GetCurrentRoom()
            => currentRoom;
    }
}