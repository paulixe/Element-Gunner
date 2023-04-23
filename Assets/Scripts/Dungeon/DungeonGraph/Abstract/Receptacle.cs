using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PII.Dungeon.DungeonGraph
{
    /// <summary>
    /// EventUser with eventUser children
    /// It will try to use the event with the children before using it with itself
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Receptacle<T> : BaseEventUser where T : IEventUser
    {
        [SerializeField][HideInInspector] protected List<T> children = new List<T>();
        public override bool TryProcessEvent(Event currentEvent, out IEventUser userOfThisEvent)
        {
            bool isEventConsumed;
            isEventConsumed = TryProcessEventFromChildren(currentEvent, out userOfThisEvent);
            if (isEventConsumed) return true;
            return TryProcessEventFromSelf(currentEvent, out userOfThisEvent);
        }
        protected virtual bool TryProcessEventFromSelf(Event currentEvent, out IEventUser userOfThisEvent)
           => base.TryProcessEvent(currentEvent, out userOfThisEvent);

        protected virtual bool TryProcessEventFromChildren(Event currentEvent, out IEventUser userOfThisEvent)
        {
            userOfThisEvent = null;
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i] == null)
                    Debug.Log("The receptacle: " + name + "has a null child");
                bool isEventConsumed = children[i].TryProcessEvent(currentEvent, out userOfThisEvent);
                if (isEventConsumed)
                {
                    return true;
                }
            }
            return false;
        }


    }
}
