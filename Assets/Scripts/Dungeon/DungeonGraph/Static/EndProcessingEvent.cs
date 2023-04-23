using System;

namespace PII.Dungeon.DungeonGraph
{
    /// <summary>
    /// Event called when we tried to use the event with every EventUser
    /// </summary>
    public static class EndProcessingEvent
    {
        public static Action<IEventUser> OnEndProcessingEvent;

    }
}