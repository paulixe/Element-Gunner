using UnityEngine;
namespace PII.Dungeon.DungeonGraph
{
    public interface IEventUser
    {
        public bool TryProcessEvent(Event currentEvent, out IEventUser EventConsumerIfEventDirty);
    }
}