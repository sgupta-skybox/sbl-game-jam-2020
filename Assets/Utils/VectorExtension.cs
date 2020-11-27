using System;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtension
{
    public static Vector3 ToVector3( this Vector2 vec2 )
    {
        return new Vector3(vec2.x, vec2.y, 0);
    }

    public static Vector3[] ToVec3Array( this Vector2[] vec2List )
    {
        List<Vector3> vec3List = new List<Vector3>();
        foreach( var vec in vec2List)
            vec3List.Add(vec.ToVector3());
        return vec3List.ToArray();
    }
}

