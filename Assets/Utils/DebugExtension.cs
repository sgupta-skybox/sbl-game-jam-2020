using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugExtension : MonoBehaviour
{
    public static void DrawCircle(Vector3 center, float radius, Color color)
    {
        int step = 32;
        float unitAngle = 2 * Mathf.PI / step;
        float angle = 0;
        float x1 = center.x + Mathf.Cos(0) * radius;
        float z1 = center.z + Mathf.Sin(0) * radius;
        for (int i = 0; i < step; i++)
        {
            angle += unitAngle;
            float x2 = center.x + Mathf.Cos(angle) * radius;
            float z2 = center.z + Mathf.Sin(angle) * radius;
            Debug.DrawLine(new Vector3(x1, center.y, z1), new Vector3(x2, center.y, z2), color, 0.1f);
            x1 = x2;
            z1 = z2;
        }
    }

    public static void DrawCircle(Vector2 center, float radius, Color color)
    {
        int step = 32;
        float unitAngle = 2 * Mathf.PI / step;
        float angle = 0;
        float x1 = center.x + Mathf.Cos(0) * radius;
        float y1 = center.y + Mathf.Sin(0) * radius;
        for (int i = 0; i < step; i++)
        {
            angle += unitAngle;
            float x2 = center.x + Mathf.Cos(angle) * radius;
            float y2 = center.y + Mathf.Sin(angle) * radius;
            Debug.DrawLine(new Vector3(x1, y1, 0), new Vector3(x2, y2, 0), color);
            x1 = x2;
            y1 = y2;
        }
    }


}
