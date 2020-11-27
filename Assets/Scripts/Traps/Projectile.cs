using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float Speed = 20.0f;

    int playerLayerMask = 0;

    void Start()
    {
        playerLayerMask = LayerMask.NameToLayer("Player");
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.up * Speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayerMask)
        {
            Controller playerController = other.gameObject.GetComponent<Controller>();
            playerController.Die();
        }
    }
}
