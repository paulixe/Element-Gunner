using System;
using UnityEngine;

namespace PII.Entities
{
    /// <summary>
    /// event called when we stop an entity 
    /// </summary>
    public class IdleEvent : MonoBehaviour
    {
        public event Action<IdleEvent, IdleEventArgs> OnIdle;
        public void Call()
        {
            OnIdle?.Invoke(this, new());
        }
    }
    public class IdleEventArgs : EventArgs
    {

    }
}