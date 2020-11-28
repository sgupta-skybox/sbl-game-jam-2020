using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 DirProjectile = Vector2.up;
    [SerializeField]
    protected float Speed = 20.0f;

    protected int playerLayerMask = 0;
    protected int buttonMask;

    protected Rigidbody2D rigidBody;

    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerLayerMask = LayerMask.NameToLayer("Player");
        buttonMask = LayerMask.NameToLayer("Button");
        DirProjectile = transform.rotation * Vector3.right;
    }

    protected virtual void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + DirProjectile * Speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayerMask)
        {
            Controller playerController = other.gameObject.GetComponent<Controller>();
            playerController.Die();
        }
        else if (other.gameObject.layer == playerLayerMask)
        {
            print("hit spawner!!");
        }
        else if (Speed > 0 && other.gameObject.layer != buttonMask)
        {
            print(other.gameObject.name);
            Destroy(gameObject);
        }
    }
}
