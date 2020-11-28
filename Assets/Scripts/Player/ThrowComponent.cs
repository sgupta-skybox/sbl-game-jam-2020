using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RequireComponent))]
public class ThrowComponent : MonoBehaviour
{
    [SerializeField]
    float ThrowSpeed = 10.0f;

    Rigidbody2D ownRigidbody2D;
    Vector2 localOffset;
    bool isGrabbed = false;
    bool isThrown = false;
    void Start()
    {
        ownRigidbody2D = GetComponent<Rigidbody2D>();
        ownRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        ownRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void OnGrabbed()
    {
        ownRigidbody2D.velocity = Vector2.zero;
        isThrown = false;
        isGrabbed = true;
        ownRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        localOffset = transform.localPosition;
    }

    public void Throw(Vector2 throwDirection)
    {
        isGrabbed = false;
        isThrown = true;
        ownRigidbody2D.velocity = throwDirection * ThrowSpeed;
    }

    void Update()
    {
        if (isGrabbed)
        {
            transform.localPosition = localOffset;
        }
    }

    void FixedUpdate()
    {
        if (isThrown && ownRigidbody2D.velocity.sqrMagnitude < 1.0f)
        {
            ownRigidbody2D.Sleep();
            isThrown = false;
            ownRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
