using UnityEditor;
using UnityEngine;
namespace PII.Dungeon.DungeonGraph
{
    /// <summary>
    /// Draws a grid in a window, the Offset is to move the grid for a satisfying interaction
    /// </summary>
    public static class GridDrawer
    {

        static float ScreenHeight => Screen.height;
        static float ScreenWidth => Screen.width;
        private static Vector2 GridOffset(float gridSize, Vector2 Offset)
        {
            return new Vector2(Offset.x % gridSize, Offset.y % gridSize);
        }
#if UNITY_EDITOR
        public static void Draw(Color lineColor, float gridSize, Vector2 Offset)
        {
            Handles.color = lineColor;
            DrawHorizontalLines(gridSize, Offset);
            DrawVerticalLines(gridSize, Offset);
            Handles.color = Color.white;
        }


        private static void DrawHorizontalLines(float gridSize, Vector2 Offset)
        {
            int linesCount = Mathf.CeilToInt(ScreenHeight / gridSize);

            for (int i = 0; i < linesCount + 1; i++)
            {
                float lineY = i * gridSize;

                Vector2 leftPoint =
                    new Vector2(-gridSize, lineY - gridSize) + GridOffset(gridSize, Offset);

                Vector2 rightPoint =
                    new Vector2(ScreenWidth + gridSize, lineY - gridSize) + GridOffset(gridSize, Offset);
                Handles.DrawLine(leftPoint, rightPoint);
            }
        }
        private static void DrawVerticalLines(float gridSize, Vector2 Offset)
        {
            int linesCount = Mathf.CeilToInt(ScreenWidth / gridSize) + 1;

            for (int i = 0; i < linesCount; i++)
            {
                float lineX = i * gridSize;
                Vector2 gridOffset = GridOffset(gridSize, Offset);


                Vector2 bottomPoint =
                    new Vector2(lineX - gridSize, -gridSize) + gridOffset;

                Vector2 TopPoint =
                    new Vector2(lineX - gridSize, ScreenHeight + gridSize) + gridOffset;

                Handles.DrawLine(bottomPoint, TopPoint);
            }
        }
#endif
    }
}