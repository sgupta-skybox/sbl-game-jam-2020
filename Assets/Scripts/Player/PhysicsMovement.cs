using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float moveSpeed = 10.0f;
    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal != 0.0f || vertical != 0.0f) {
            Vector2 newDir = new Vector2(horizontal, vertical);
            rigidBody.MovePosition(rigidBody.position + newDir * Time.fixedDeltaTime * moveSpeed);
        }
    }
}
