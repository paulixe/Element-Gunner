using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
namespace PII.Dungeon.DungeonGraph
{
    /// <summary>
    /// DungeonGraph Window
    /// </summary>
    public class DungeonGraphEditor : EditorWindow
    {
        private static DungeonGraphSO currentDungeonGraph;



        //[MenuItem("Edit Graph", menuItem = "Tools/PII/Edit Dungeon Graph")]
        public static void OpenWindow()
        {
            GetWindow<DungeonGraphEditor>("Dungeon Graph Editor");
        }
        [OnOpenAsset(0)]
        public static bool OnDoubleClickAsset(int instanceID, int line)
        {
            DungeonGraphSO dungeonGraphClickedOn = EditorUtility.InstanceIDToObject(instanceID) as DungeonGraphSO;

            if (dungeonGraphClickedOn != null)
            {
                currentDungeonGraph = dungeonGraphClickedOn;
                OpenWindow();
                currentDungeonGraph.OnValidate();
                return true;
            }
            return false;
        }
        private void OnEnable()
        {


        }
        private void OnGUI()
        {
            if (currentDungeonGraph == null)
                Debug.LogError(nameof(currentDungeonGraph) + " isn't assigned in DungeonGraphEditor");

            Event currentEvent = Event.current;
            currentDungeonGraph.Draw(currentEvent);
            currentDungeonGraph.TryProcessEvent(currentEvent, out IEventUser eventUser);
            EndProcessingEvent.OnEndProcessingEvent?.Invoke(eventUser);
            GUI.changed = true;
        }
    }
}

