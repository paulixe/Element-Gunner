using System;
using UnityEngine;
namespace PII.Dungeon.DungeonGraph
{
    /// <summary>
    /// Abstract class for assigning each event to a function
    /// </summary>
    public abstract class BaseEventUser : ScriptableObject, IEventUser
    {
        public virtual bool TryProcessEvent(Event currentEvent, out IEventUser EventConsumerIfEventDirty)
        {
            return
                currentEvent.type switch
                {
                    EventType.KeyDown => ProcessKeyEvents(currentEvent, out EventConsumerIfEventDirty),
                    EventType.MouseDown => ProcessMouseDown(currentEvent, out EventConsumerIfEventDirty),
                    EventType.MouseUp => ProcessMouseUp(currentEvent, out EventConsumerIfEventDirty),
                    EventType.MouseDrag => ProcessMouseDrag(currentEvent, out EventConsumerIfEventDirty),
                    EventType.ScrollWheel => ProcessMouseWheelScroll(currentEvent, out EventConsumerIfEventDirty),
                    _ => CantConsumeEvent(out EventConsumerIfEventDirty),
                };
        }

        protected virtual bool ConsumeEvent(Event currentEvent, out IEventUser userOfThisEvent)
        {
            userOfThisEvent = this;
            return true;
        }
        protected virtual bool CantConsumeEvent(out IEventUser userOfThisEvent)
        {

            userOfThisEvent = null;
            return false;
        }
        #region KeyEvents
        bool ProcessKeyEvents(Event currentEvent, out IEventUser userOfThisEvent)
        {
            if (currentEvent.modifiers == EventModifiers.Shift)
            {
                return ProcessKeyEventsWithModifiers(currentEvent, out userOfThisEvent);
            }
            else
            {
                return ProcessOneKeyEvents(currentEvent, out userOfThisEvent);
            }
        }
        bool ProcessOneKeyEvents(Event currentEvent, out IEventUser userOfThisEvent) =>
              currentEvent.keyCode switch
              {
                  KeyCode.Delete => ProcessDelKey(currentEvent, out userOfThisEvent),
                  KeyCode.A => ProcessAKey(currentEvent, out userOfThisEvent),
                  _ => CantConsumeEvent(out userOfThisEvent),
              };

        bool ProcessKeyEventsWithModifiers(Event currentEvent, out IEventUser userOfThisEvent) =>
            currentEvent.keyCode switch
            {
                _ => CantConsumeEvent(out userOfThisEvent),
            };
        protected virtual bool ProcessAKey(Event currentEvent, out IEventUser userOfThisEvent)
        {
            return CantConsumeEvent(out userOfThisEvent);
        }
        protected virtual bool ProcessDelKey(Event currentEvent, out IEventUser userOfThisEvent)
        {
            return CantConsumeEvent(out userOfThisEvent);
        }
        #endregion
        #region AssignMouseEvents
        bool ProcessMouseDown(Event currentEvent, out IEventUser userOfThisEvent)
        {
            if (currentEvent.button == 0)
            {
                return ProcessLeftMouseDown(currentEvent, out userOfThisEvent);
            }
            else if (currentEvent.button == 1)
            {
                return ProcessRightMouseDown(currentEvent, out userOfThisEvent);
            }
            else if (currentEvent.button == 2)
            {
                return ProcessMouseWheelDown(currentEvent, out userOfThisEvent);
            }
            throw new Exception("We didn't find any mouse down event in current event");
        }
        bool ProcessMouseUp(Event currentEvent, out IEventUser userOfThisEvent)
        {
            if (currentEvent.button == 0)
            {
                return ProcessLeftMouseUp(currentEvent, out userOfThisEvent);
            }
            else if (currentEvent.button == 1)
            {
                return ProcessRightMouseUp(currentEvent, out userOfThisEvent);
            }
            else if (currentEvent.button == 2)
            {
                return ProcessMouseWheelUp(currentEvent, out userOfThisEvent);
            }
            throw new Exception("We didn't find any mouse up event in current event");
        }
        bool ProcessMouseDrag(Event currentEvent, out IEventUser userOfThisEvent)
        {
            if (currentEvent.button == 0)
            {
                return ProcessLeftMouseDrag(currentEvent, out userOfThisEvent);
            }
            else if (currentEvent.button == 1)
            {
                return ProcessRightMouseDrag(currentEvent, out userOfThisEvent);
            }
            else if (currentEvent.button == 2)
            {
                return ProcessMouseWheelDrag(currentEvent, out userOfThisEvent);
            }
            throw new Exception("We didn't find any mouse drag event in current event");
        }
        #endregion
        #region MouseLeft
        protected virtual bool ProcessLeftMouseDown(Event currentEvent, out IEventUser userOfThisEvent)
        {
            return CantConsumeEvent(out userOfThisEvent);
        }
        protected virtual bool ProcessLeftMouseUp(Event currentEvent, out IEventUser userOfThisEvent)
        {
            return CantConsumeEvent(out userOfThisEvent);
        }
        protected virtual bool ProcessLeftMouseDrag(Event currentEvent, out IEventUser userOfThisEvent)
        {
            return CantConsumeEvent(out userOfThisEvent);
        }
        #endregion
        #region MouseRight

        protected virtual bool ProcessRightMouseDown(Event currentEvent, out IEventUser userOfThisEvent)
        {
            return CantConsumeEvent(out userOfThisEvent);
        }

        protected virtual bool ProcessRightMouseUp(Event currentEvent, out IEventUser userOfThisEvent)
        {
            return CantConsumeEvent(out userOfThisEvent);
        }
        protected virtual bool ProcessRightMouseDrag(Event currentEvent, out IEventUser userOfThisEvent)
        {
            return CantConsumeEvent(out userOfThisEvent);
        }
        #endregion
        #region MouseWheel
        protected virtual bool ProcessMouseWheelDown(Event currentEvent, out IEventUser userOfThisEvent)
        {
            return CantConsumeEvent(out userOfThisEvent);
        }
        protected virtual bool ProcessMouseWheelUp(Event currentEvent, out IEventUser userOfThisEvent)
        {
            return CantConsumeEvent(out userOfThisEvent);
        }
        protected virtual bool ProcessMouseWheelDrag(Event currentEvent, out IEventUser userOfThisEvent)
        {
            return CantConsumeEvent(out userOfThisEvent);
        }
        protected virtual bool ProcessMouseWheelScroll(Event currentEvent, out IEventUser userOfThisEvent)
        {
            return CantConsumeEvent(out userOfThisEvent);
        }
        #endregion
    }
}
