using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Mathf;
namespace PII.Entities
{
    /// <summary>
    /// Custom editor of FireModule for drawing the angles in a protractor
    /// </summary>
    [CustomEditor(typeof(FireModule))]
    public class FireModuleEditor : Editor
    {
        const int CIRCLE_RADIUS = 100;
        const int DOT_RADIUS = 20;
        Texture2D buttonOffTexture;
        Texture2D buttonOnTexture;
        private void OnEnable()
        {
            buttonOffTexture = EditorGUIUtility.Load("d_Button Icon") as Texture2D;
            buttonOnTexture = EditorGUIUtility.Load("Button Icon") as Texture2D;
        }

        /// <summary>
        /// Create the dot corresponding to an angle in a protractor.
        /// Returns the new angle if the user moved it
        /// </summary>
        /// <param name="controlId"></param>
        /// <param name="protractorCenter"></param>
        /// <param name="angle"></param>
        /// <param name="protractorRadius"></param>
        /// <param name="dotRadius"></param>
        /// <returns></returns>
        private float DrawDot(int controlId, Vector2 protractorCenter, float angle, float protractorRadius, float dotRadius)
        {
            Vector2 circleDirection = new Vector2(Cos(angle), Sin(angle));
            Vector2 dotSize = dotRadius * Vector2.one;
            Vector2 dotPosition = protractorCenter + (protractorRadius - dotRadius) * circleDirection - dotSize / 2;
            Rect pointRect = new Rect(dotPosition, dotSize);

            switch (Event.current.GetTypeForControl(controlId))
            {
                case EventType.Repaint:
                    {
                        Handles.BeginGUI();

                        Texture2D buttonIcon = null;
                        if (controlId == GUIUtility.hotControl)
                            buttonIcon = buttonOffTexture;
                        else
                            buttonIcon = buttonOnTexture;

                        GUI.DrawTexture(pointRect, buttonIcon);
                        Handles.EndGUI();
                        break;
                    }
                case EventType.MouseDown:
                    {
                        if (pointRect.Contains(Event.current.mousePosition))
                        {
                            GUIUtility.hotControl = controlId;
                            Event.current.Use();
                            GUI.FocusControl("");
                            GUI.changed = true;
                        }

                        break;
                    }
                case EventType.MouseUp:
                    {
                        if (controlId == GUIUtility.hotControl)
                        {
                            GUIUtility.hotControl = 0;
                            Event.current.Use();
                            GUI.changed = true;
                        }
                        break;
                    }
            }
            if (Event.current.isMouse && controlId == GUIUtility.hotControl)
            {
                Vector2 mousePos = Event.current.mousePosition;
                Vector2 protractorToMouse = mousePos - protractorCenter;

                float newAngle = Atan2(protractorToMouse.y, protractorToMouse.x);
                Event.current.Use();
                GUI.changed = true;
                return newAngle;
            }
            return angle;
        }
        private void DrawDisc(Vector2 position, float radius)
        {
            Handles.DrawSolidDisc(position, Vector3.forward, radius);
        }
        public override void OnInspectorGUI()
        {
            FireModule fireModule = (FireModule)target;
            List<float> angles = fireModule.angles;
            int[] controlIds = new int[angles.Count];
            for (int i = 0; i < controlIds.Length; i++)
            {
                controlIds[i] = GUIUtility.GetControlID(FocusType.Passive);
            }

            Rect circleRect = EditorGUILayout.GetControlRect(false, CIRCLE_RADIUS * 2);
            DrawDisc(circleRect.center, CIRCLE_RADIUS);

            for (int i = 0; i < controlIds.Length; i++)
            {
                int controlId = controlIds[i];
                float angle = angles[i];

                float newAngle = DrawDot(controlId, circleRect.center, angle, CIRCLE_RADIUS, DOT_RADIUS);
                angles[i] = newAngle;
            }

            DrawDefaultInspector();
        }
    }
}