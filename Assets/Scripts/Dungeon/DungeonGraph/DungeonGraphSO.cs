using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Utilities.ValidateUtilities;
namespace PII.Dungeon.DungeonGraph
{
    /// <summary>
    /// Represents a dungeon graph
    /// </summary>
    [CreateAssetMenu(fileName = nameof(DungeonGraphSO) + "_", menuName = "Scriptable Objects/PII/Dungeon/" + nameof(DungeonGraphSO))]
    public class DungeonGraphSO : Receptacle<RoomNode>, IDrawable
    {
        public int ChildrenCount => children.Count;
        private Vector2 gridOffset;
        public RoomNode GetEntrance() => children.Find(node => node.roomType == RoomType.Entrance);
#if UNITY_EDITOR
        private GenericMenu CreateGenericMenu(Vector2 position)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Create Node"), false, CreateRoomNodeAt, position);
            menu.AddItem(new GUIContent("Delete selected links"), false, DeleteSelectedLinks);
            return menu;
        }

        private void DeleteSelectedLinks()
        {
            foreach (RoomNode roomNode in children)
            {
                if (roomNode.IsSelected)
                {
                    Queue<RoomNode> childrenToRemove = new Queue<RoomNode>();
                    foreach (RoomNode child in roomNode.children)
                        if (child.IsSelected)
                            childrenToRemove.Enqueue(child);
                    roomNode.RemoveChildrenLinks(childrenToRemove);
                }
            }
        }

        private void CreateRoomNodeAt(object mousePositionObject) => CreateRoomNodeAt((Vector2)mousePositionObject);
        private void CreateRoomNodeAt(Vector2 position)
        {
            RoomNode roomNode = CreateInstance<RoomNode>();
            roomNode.Initialise(position, this);

            children.Add(roomNode);
            AssetDatabase.AddObjectToAsset(roomNode, this);
            AssetDatabase.SaveAssets();
        }

        public void Draw(Event currentEvent)
        {
            GridDrawer.Draw(DungeonGraphResources.Instance.SmallGridColor, DungeonGraphSettings.SMALL_GRID_SIZE, gridOffset);
            GridDrawer.Draw(DungeonGraphResources.Instance.LargeGridColor, DungeonGraphSettings.LARGE_GRID_SIZE, gridOffset);
            for (int i = children.Count - 1; i >= 0; i--)
                children[i].DrawLinks(currentEvent);
            for (int i = children.Count - 1; i >= 0; i--)
                children[i].Draw(currentEvent);
        }
        public void DeleteNode(RoomNode roomNode)
        {
            foreach (RoomNode child in roomNode.children)
            {
                child.parents.Remove(roomNode);
            }
            foreach (RoomNode parent in roomNode.parents)
            {
                parent.children.Remove(roomNode);
            }
            children.Remove(roomNode);
            AssetDatabase.RemoveObjectFromAsset(roomNode);
            DestroyImmediate(roomNode, true);
            AssetDatabase.SaveAssets();
        }




        private void ProcessChildFirst(RoomNode roomNode)
        {
            children.Remove(roomNode);
            children.Insert(0, roomNode);
        }
        private void DragOf(Vector2 delta)
        {
            foreach (RoomNode roomNode in children)
                roomNode.DragOf(delta);
            gridOffset += delta;
        }
        private void DeselectEverything()
        {
            foreach (RoomNode roomNode in children)
                roomNode.IsSelected = false;
        }
        protected override bool TryProcessEventFromChildren(Event currentEvent, out IEventUser userOfThisEvent)
        {
            bool isEventUsedByChildren = base.TryProcessEventFromChildren(currentEvent, out userOfThisEvent);

            if (isEventUsedByChildren)
            {
                ProcessChildFirst(userOfThisEvent as RoomNode);
            }
            return isEventUsedByChildren;
        }
        protected override bool ProcessRightMouseDown(Event currentEvent, out IEventUser userOfThisEvent)
        {
            GenericMenu contextMenu = CreateGenericMenu(currentEvent.mousePosition);
            contextMenu.ShowAsContext();
            return ConsumeEvent(currentEvent, out userOfThisEvent);
        }
        protected override bool ProcessLeftMouseDown(Event currentEvent, out IEventUser userOfThisEvent)
        {
            DeselectEverything();
            return base.ProcessLeftMouseDown(currentEvent, out userOfThisEvent);
        }
        protected override bool ProcessLeftMouseDrag(Event currentEvent, out IEventUser userOfThisEvent)
        {
            DragOf(currentEvent.delta);
            return base.ProcessLeftMouseDrag(currentEvent, out userOfThisEvent);
        }
        #region validation
        public void OnValidate()
        {
            foreach (RoomNode child in children)
            {
                if (child.parents.Count == 0 && child.roomType != RoomType.Entrance)
                {
                    Debug.Log("The node " + child.name + " has no parents in the graph " + name);
                }
            }

            int bossRoomCount = 0;
            int entranceRoomCount = 0;
            foreach (RoomNode roomNode in children)
            {
                if (roomNode.roomType == RoomType.BossRoom)
                    bossRoomCount++;
                if (roomNode.roomType == RoomType.Entrance)
                    entranceRoomCount++;
            }

            if (bossRoomCount == 0)
                Debug.Log("There are no " + nameof(RoomNode) + " with a " + nameof(RoomType) + " of " + RoomType.BossRoom.ToString() + " in " + name);
            if (entranceRoomCount == 0)
                Debug.Log("There are no " + nameof(RoomNode) + " with a " + nameof(RoomType) + " of " + RoomType.Entrance.ToString() + " in " + name);
            if (bossRoomCount > 1)
                Debug.Log("There are more than 1 " + RoomType.BossRoom.ToString() + " " + nameof(RoomNode) + " in " + name);
            if (entranceRoomCount > 1)
                Debug.Log("There are more than 1 " + RoomType.Entrance.ToString() + " " + nameof(RoomNode) + " in " + name);
            ValidateCheckEnumerableValues(this, nameof(children), children);

        }
        #endregion
#endif
    }
}