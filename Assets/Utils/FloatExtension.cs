using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatExtension
{
    public static bool IsClosed( this float lhs, float rhs, float delta = float.Epsilon )
    {
        return Mathf.Abs(lhs - rhs) <= delta;
    }
}
