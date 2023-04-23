
using UnityEngine;
using static UnityEngine.Mathf;
namespace Utilities
{
    /// <summary>
    /// Functions used often in Unity projects
    /// </summary>
    public static class CommonUtilities
    {
        public static Camera MainCamera;
        public static Vector3 GetMouseWorldPos()
        {
            if (MainCamera == null) MainCamera = Camera.main;

            Vector3 mouseScreenPosition = Input.mousePosition;

            mouseScreenPosition.x = Clamp(mouseScreenPosition.x, 0, Screen.width);
            mouseScreenPosition.y = Clamp(mouseScreenPosition.y, 0, Screen.height);

            Vector3 worldPos = MainCamera.ScreenToWorldPoint(mouseScreenPosition);
            return worldPos;
        }
        public static float GetAngleFromVector(Vector3 vector)
        {
            float radians = Atan2(vector.y, vector.x);
            float degrees = radians * Rad2Deg;
            return degrees;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="angle">angle in radians</param>
        /// <returns></returns>
        public static Vector2 GetVectorFromAngle(float angle)
        {
            angle = angle % (2 * PI);

            return new(Cos(angle), Sin(angle));
        }
        public static bool IsOverlapping(float xMin1, float xMax1, float xMin2, float xMax2)
            => Max(xMin1, xMin2) <= Min(xMax1, xMax2);

    }
}