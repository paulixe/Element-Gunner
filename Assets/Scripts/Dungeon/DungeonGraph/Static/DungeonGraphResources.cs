using UnityEditor;
using UnityEngine;
namespace PII.Dungeon.DungeonGraph
{
    /// <summary>
    /// Data used for editing node graphs in the editor
    /// </summary>
    [CreateAssetMenu(fileName = nameof(DungeonGraphResources), menuName = "Scriptable Objects/PII/Dungeon/" + nameof(DungeonGraphResources))]
    public class DungeonGraphResources : ScriptableObject
    {
#if UNITY_EDITOR
        private static DungeonGraphResources instance;
        public static DungeonGraphResources Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<DungeonGraphResources>(nameof(DungeonGraphResources));
                }
                return instance;
            }
        }

        private void OnEnable()
        {
            NodeRoomDrawStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
            NodeRoomSelectedStyle.normal.background = EditorGUIUtility.Load("node1 on") as Texture2D;
        }

        public GUIStyle NodeRoomDrawStyle;
        public GUIStyle NodeRoomSelectedStyle;
        public Color SmallGridColor = new Color(0.5f, 0.5f, 0.5f, 0.4f);
        public Color LargeGridColor = new Color(0.5f, 0.5f, 0.5f, 0.6f);
#endif
    }
}