using System;
using System.Collections.Generic;
using UnityEngine;

public class RotatingProjectileSpawner  : ProjectileSpawner
{
    [SerializeField]
    float RotatingTime = 3.0f;

    float curTime = 0.0f;

    private void Update()
    {
        curTime += Time.deltaTime;
        if (curTime > RotatingTime)
            curTime -= RotatingTime;
        var angle = curTime / RotatingTime * 360.0f ;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}

