using UnityEngine;
namespace PII.Dungeon.DungeonGraph
{
    public interface IDrawable
    {
#if UNITY_EDITOR
        public void Draw(Event currentEvent);
#endif
    }
}