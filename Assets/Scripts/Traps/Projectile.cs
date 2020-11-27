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

    void Start()
    {
        playerLayerMask = LayerMask.NameToLayer("Player");
        buttonMask = LayerMask.NameToLayer("Button");
    }

    protected virtual void FixedUpdate()
    {
        transform.Translate(DirProjectile * Speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayerMask)
        {
            Controller playerController = other.gameObject.GetComponent<Controller>();
            playerController.Die();
        }
        if (Speed > 0 && other.gameObject.layer != buttonMask)
        {
            Destroy(gameObject);
        }
    }
}
