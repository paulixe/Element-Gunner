using System;
using UnityEngine;
namespace PII.Entities
{
    /// <summary>
    /// Event called when a health component has no more health
    /// </summary>
    public class DestroyedEvent : MonoBehaviour
    {
        public event Action<DestroyedEvent, DestroyedEventArgs> OnDestroy;
        public void Call()
        {
            DestroyedEventArgs eventArgs = new();
            OnDestroy?.Invoke(this, eventArgs);
        }
    }
    public class DestroyedEventArgs : EventArgs
    {
        public DestroyedEventArgs()
        {

        }
    }
}
