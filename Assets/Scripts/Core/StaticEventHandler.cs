using PII.Dungeon;
using System;
namespace PII
{
    /// <summary>
    /// Event handler for global events
    /// </summary>
    public static class StaticEventHandler
    {
        public static event Action<RoomChangedEventArgs> OnRoomChange;
        public static void ChangeRoom(InstantiatedRoom roomEntered)
        {
            OnRoomChange?.Invoke(new(roomEntered));
        }

        public static event Action<GameStateChangeEventArgs> OnGameStateChange;
        public static void ChangeGameState(GameState state)
        {
            OnGameStateChange?.Invoke(new(state));
        }
        public static event Action<RoomClearedEventArgs> OnRoomCleared;
        public static void CallRoomCleared()
        {
            OnRoomCleared?.Invoke(new());
        }
    }
    public class GameStateChangeEventArgs : EventArgs
    {
        public GameState State;
        public GameStateChangeEventArgs(GameState state)
        {
            this.State = state;
        }
    }
    public class RoomChangedEventArgs : EventArgs
    {
        public InstantiatedRoom CurrentRoom;
        public RoomChangedEventArgs(InstantiatedRoom instantiatedRoom)
        {
            CurrentRoom = instantiatedRoom;
        }
    }
    public class RoomClearedEventArgs : EventArgs
    {

        public RoomClearedEventArgs()
        {
        }
    }
}
