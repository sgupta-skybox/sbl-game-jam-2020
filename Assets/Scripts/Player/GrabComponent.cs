using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabComponent : MonoBehaviour
{
    public float moveForce = 500;

    new BoxCollider2D collider;
    Rigidbody2D rigidBody;
    bool isAttached;
    Vector2 contactNormal;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if( Input.GetKeyUp(KeyCode.Space) && isAttached ) {
            isAttached = false;
            transform.SetParent(null);
            rigidBody.simulated = true;
            rigidBody.AddForce(contactNormal * moveForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( isAttached == false && collision.gameObject.CompareTag("Player"))
        {
            rigidBody.velocity = Vector2.zero;
            rigidBody.simulated = false;
            contactNormal = collision.contacts[0].normal;
            transform.SetParent(collision.gameObject.transform);
            isAttached = true;
        }
    }
}
