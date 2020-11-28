using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ThrowComponent : MonoBehaviour
{
    [SerializeField]
    float ThrowSpeed = 10.0f;

    Rigidbody2D ownRigidbody2D;
    Vector2 localOffset;
    void Start()
    {
        ownRigidbody2D = GetComponent<Rigidbody2D>();
        ownRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        ownRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        ownRigidbody2D.gravityScale = 0.0f;
        ownRigidbody2D.drag = 5.0f;
        ownRigidbody2D.angularDrag = 0.0f;
    }

    public void OnGrabbed()
    {
        ownRigidbody2D.velocity = Vector2.zero;
        ownRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        localOffset = transform.localPosition;
    }

    public void Throw(Vector2 throwDirection)
    {
        ownRigidbody2D.velocity = throwDirection * ThrowSpeed;
    }

    void Update()
    {
        if (transform.parent)
        {
            transform.localPosition = localOffset;
        }
    }

    void FixedUpdate()
    {
        if (!transform.parent && ownRigidbody2D.velocity.sqrMagnitude < 1.0f)
        {
            ownRigidbody2D.Sleep();
            ownRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
