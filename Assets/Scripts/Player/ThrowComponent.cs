using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ThrowComponent : MonoBehaviour
{
    [SerializeField]
    float ThrowSpeed = 10.0f;

    Rigidbody2D ownRigidbody2D;
    // HACK : hacky way to check if this component has thrown way
    // to determine make sleep when speed goes down.
    bool isThrew = false;
    bool hasThrown = false;
    void Start()
    {
        ownRigidbody2D = GetComponent<Rigidbody2D>();
        ownRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        ownRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        ownRigidbody2D.gravityScale = 0.0f;
        ownRigidbody2D.drag = 5.0f;
        ownRigidbody2D.angularDrag = 0.0f;
    }

    public void OnGrabbed(Controller parent)
    {
        ownRigidbody2D.velocity = Vector2.zero;
        ownRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        parent.OnGrabReleased += GrabReleased;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), transform.parent.GetComponent<Collider2D>());
    }

    public void Throw(Vector2 throwDirection)
    {
        print(throwDirection);
        //ownRigidbody2D.velocity = throwDirection * ThrowSpeed;
        print(ownRigidbody2D.velocity);
        // rigid body would lose its velocity in some certain cases
        // AddForce gives its force more reliably.
        ownRigidbody2D.AddForce(throwDirection * ThrowSpeed * 50);
        isThrew = true;
    }

    void GrabReleased(Controller parent)
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), parent.GetComponent<Collider2D>(), false);
        parent.OnGrabReleased -= GrabReleased;
    }

    void Update()
    {
        if (transform.parent)
        {
            var controller = transform.parent.GetComponent<Controller>();
            if (controller)
            {
                var dir = controller.movementVelocity.normalized;
                if (dir != Vector2.zero)
                {
                    ownRigidbody2D.MovePosition( transform.parent.position + dir.ToVector3() * controller.ColliderRadius * 2);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (!transform.parent)
        {
            if (isThrew && ownRigidbody2D.velocity.sqrMagnitude > 1.0f)
            {
                isThrew = false;
                hasThrown = true;
            }
            if (hasThrown && ownRigidbody2D.velocity.sqrMagnitude < 1.0f)
            {
                ownRigidbody2D.Sleep();
                hasThrown = false;
            }
            if ( ownRigidbody2D.IsSleeping())
            {
                ownRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }
}
