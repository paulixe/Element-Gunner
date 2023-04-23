using PII.Entities;
using UnityEngine;
namespace PII
{
    /// <summary>
    /// Functions specific to the project
    /// </summary>
    public static class ProjectUtilities
    {
        public static AimDirection GetAimDirection(float degrees)
        {
            if (degrees > 22 && degrees < 67)
                return AimDirection.UpRight;
            else if (degrees > 67 && degrees < 112)
                return AimDirection.Up;
            else if (degrees > 112 && degrees < 158)
                return AimDirection.UpLeft;
            else if (degrees > 158 || degrees < -135)
                return AimDirection.Left;
            else if (degrees > -135 && degrees < -45)
                return AimDirection.Down;
            return AimDirection.Right;
        }
    }
}