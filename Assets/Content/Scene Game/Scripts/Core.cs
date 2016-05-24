using UnityEngine;
using System.Collections;

public static class Core 
{
    public static float ToAngle(this Vector2 angle)
    {
        return Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
    }
}
