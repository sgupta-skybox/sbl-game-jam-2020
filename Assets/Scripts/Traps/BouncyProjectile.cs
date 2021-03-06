﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyProjectile : Projectile
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (rigidBody)
            rigidBody.AddForce(DirProjectile * Speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == playerLayerMask)
        {
            Controller playerController = collision.gameObject.GetComponent<Controller>();
            playerController.Die();
            Destroy(gameObject);
        }
   }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
    }
}
