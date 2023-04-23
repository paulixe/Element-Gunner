using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace PII.Dungeon.DungeonGraph
{
    /// <summary>
    /// node in the dungeon graph, we can edit its Room type in the editor 
    /// </summary>
    public class RoomNode : BaseEventUser, IDrawable
    {

        [SerializeField, HideInInspector] private DungeonGraphSO graph;
#if UNITY_EDITOR
        [SerializeField, HideInInspector] private Rect Rect;
        [SerializeField, HideInInspector] private GUIStyle drawStyle;
        [SerializeField, HideInInspector] private GUIStyle selectedStyle;
        [HideInInspector] public bool IsSelected;
        public Vector2 GetRectCenter() => Rect.center;
#endif

        public List<RoomNode> parents = new List<RoomNode>();
        public List<RoomNode> children = new List<RoomNode>();
        public RoomType roomType;
        private bool isDragged;
        private bool isLinkingToAnother;


        public bool IsCorridor => roomType == RoomType.CorridorNS || roomType == RoomType.CorridorEW;

#if UNITY_EDITOR
        public void RemoveChild(RoomNode child)
        {
            children.Remove(child);
        }
        public void RemoveParent(RoomNode parent)
        {
            parents.Remove(parent);
        }
        public void Initialise(Vector2 position, DungeonGraphSO graph)
        {
            name = nameof(RoomNode);
            Rect = new Rect(position, new Vector2(DungeonGraphSettings.NODE_WIDTH, DungeonGraphSettings.NODE_HEIGHT));
            selectedStyle = DungeonGraphResources.Instance.NodeRoomSelectedStyle;
            drawStyle = DungeonGraphResources.Instance.NodeRoomDrawStyle;
            this.graph = graph;
        }
        public void Draw(Event currentEvent)
        {
            GUILayout.BeginArea(Rect, IsSelected ? selectedStyle : drawStyle);
            EditorGUI.BeginChangeCheck();

            RoomType newRoomType = (RoomType)EditorGUILayout.EnumPopup(roomType);

            bool hasChangedRoomType = newRoomType != roomType;
            if (hasChangedRoomType)
                VerifyLinksValidity();
            roomType = newRoomType;

            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(this);
            GUILayout.EndArea();
        }
        public void VerifyLinksValidity()
        {
            Queue<RoomNode> childrenToRemove = new Queue<RoomNode>();
            foreach (RoomNode child in children)
                if (!CanBeParentOf(child))
                    childrenToRemove.Enqueue(child);
            RemoveChildrenLinks(childrenToRemove);

            Queue<RoomNode> parentsToVerify = new Queue<RoomNode>(parents);
            while (parentsToVerify.TryDequeue(out RoomNode parent))
                parent.VerifyLinksValidity();
        }
        public void RemoveChildrenLinks(Queue<RoomNode> childrenToRemove)
        {
            while (childrenToRemove.TryDequeue(out RoomNode child))
            {
                child.RemoveParent(this);
                RemoveChild(child);
            }
        }
        public void DrawLinks(Event currentEvent)
        {
            if (isLinkingToAnother)
                Handles.DrawLine(GetRectCenter(), currentEvent.mousePosition);

            foreach (RoomNode node in children)
                DrawLineTo(node);
        }
        private void DrawLineTo(RoomNode otherNode)
        {
            float arrowVerticalSize = DungeonGraphSettings.LINK_ARROW_VERTICAL_SIZE;
            float arrowHorizontalSize = DungeonGraphSettings.LINK_ARROW_HORIZONTAL_SIZE;

            Vector2 pt2 = otherNode.GetRectCenter();
            Vector2 pt1 = GetRectCenter();
            Vector2 pt1_to_pt2 = pt2 - pt1;
            Vector2 perpendicularDirection = new Vector2(-pt1_to_pt2.y, pt1_to_pt2.x).normalized;
            Vector2 middle = pt1 + (pt1_to_pt2) / 2;

            Vector2 arrowPt1 = middle + perpendicularDirection * arrowVerticalSize;
            Vector2 arrowPt2 = middle - perpendicularDirection * arrowVerticalSize;
            Vector2 arrowEnd = middle + pt1_to_pt2.normalized * arrowHorizontalSize;

            if (IsSelected && otherNode.IsSelected)
                Handles.color = Color.red;
            Handles.DrawLine(arrowPt1, arrowEnd);
            Handles.DrawLine(arrowPt2, arrowEnd);

            Handles.DrawLine(pt1, pt2);
            Handles.color = Color.white;
        }
        public bool IsOverRect(Vector2 position)
        {
            return Rect.Contains(position);
        }
        public void DragOf(Vector2 delta)
        {
            Rect.position += delta;
            EditorUtility.SetDirty(this);
        }

        private GenericMenu CreateGenericMenu(Vector2 position)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Delete Node"), false, DeleteNodeDelayed);
            return menu;
        }

        private void DeleteNodeDelayed()
        {
            EndProcessingEvent.OnEndProcessingEvent += DeleteNodeCallback;
        }
        private void DeleteNodeCallback(IEventUser eventUser)
        {
            try
            {
                graph.DeleteNode(this);
            }
            catch
            {

            }
            EndProcessingEvent.OnEndProcessingEvent -= DeleteNodeCallback;
        }
        private bool CanBeParentOf(RoomNode otherNode)
        {
            if (otherNode is null)
                return false;
            if (otherNode == this)
                return false;
            if (children.Contains(otherNode))
                return false;
            if (otherNode.parents.Contains(this))
                return false;

            //Only 1 parent for each node
            if (otherNode.parents.Count > 0)
                return false;
            if ((IsCorridor && otherNode.IsCorridor) || ((!IsCorridor) && (!otherNode.IsCorridor)))
                return false;
            if (children.Count >= 4)
                return false;

            return true;
        }
        private void LinkToCallBack(IEventUser eventUser)
        {
            RoomNode otherNode = eventUser as RoomNode;
            if (CanBeParentOf(otherNode))
            {
                children.Add(otherNode);
                otherNode.parents.Add(this);
            }
            EndProcessingEvent.OnEndProcessingEvent -=
                LinkToCallBack;
        }
        #region Event
        protected override bool ProcessMouseWheelDown(Event currentEvent, out IEventUser userOfThisEvent)
        {
            if (IsOverRect(currentEvent.mousePosition))
            {
                isLinkingToAnother = true;
                return ConsumeEvent(currentEvent, out userOfThisEvent);
            }
            return CantConsumeEvent(out userOfThisEvent);
        }
        protected override bool ProcessMouseWheelUp(Event currentEvent, out IEventUser userOfThisEvent)
        {
            if (isLinkingToAnother)
            {
                isLinkingToAnother = false;
                EndProcessingEvent.OnEndProcessingEvent += LinkToCallBack;
                return CantConsumeEvent(out userOfThisEvent);
            }
            if (IsOverRect(currentEvent.mousePosition))
                return ConsumeEvent(currentEvent, out userOfThisEvent);

            return CantConsumeEvent(out userOfThisEvent);
        }
        protected override bool ProcessMouseWheelDrag(Event currentEvent, out IEventUser userOfThisEvent)
        {
            if (isLinkingToAnother)
                return ConsumeEvent(currentEvent, out userOfThisEvent);
            return CantConsumeEvent(out userOfThisEvent);
        }
        protected override bool ProcessRightMouseDown(Event currentEvent, out IEventUser userOfThisEvent)
        {
            if (IsOverRect(currentEvent.mousePosition))
            {
                GenericMenu contextMenu = CreateGenericMenu(currentEvent.mousePosition);
                contextMenu.ShowAsContext();
                return ConsumeEvent(currentEvent, out userOfThisEvent);
            }
            return base.ProcessRightMouseDown(currentEvent, out userOfThisEvent);
        }
        protected override bool ProcessLeftMouseDown(Event currentEvent, out IEventUser userOfThisEvent)
        {
            if (IsOverRect(currentEvent.mousePosition))
            {
                isDragged = true;
                Selection.activeObject = this;
                IsSelected = !IsSelected;
                return ConsumeEvent(currentEvent, out userOfThisEvent);
            }


            return base.ProcessLeftMouseDown(currentEvent, out userOfThisEvent);
        }
        protected override bool ProcessLeftMouseDrag(Event currentEvent, out IEventUser userOfThisEvent)
        {
            if (isDragged)
            {
                DragOf(currentEvent.delta);
                return ConsumeEvent(currentEvent, out userOfThisEvent);
            }
            return base.ProcessLeftMouseDrag(currentEvent, out userOfThisEvent);
        }
        protected override bool ProcessLeftMouseUp(Event currentEvent, out IEventUser userOfThisEvent)
        {
            isDragged = false;
            return base.ProcessLeftMouseUp(currentEvent, out userOfThisEvent);
        }
        #endregion
#endif
    }
}
