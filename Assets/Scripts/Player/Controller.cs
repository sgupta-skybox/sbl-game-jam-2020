using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]
    float Speed = 10.0f;

    [SerializeField]
    [Range(1.0f, 2.0f)]
    float GrabRadiusMultiplier = 1.2f;

    Vector2 movementVelocity;

    GrabComponent grabComponent;
    CircleCollider2D controllerCollider;
    Rigidbody2D controllerBody;

    Transform spriteChild;
    float spriteAngle = 0.0f;

    [SerializeField]
    float SpriteTurnSpeed = 5.0f;
 
    void Start()
    {
        movementVelocity = Vector2.zero;
        controllerCollider = GetComponent<CircleCollider2D>();
        controllerBody = GetComponent<Rigidbody2D>();
        spriteChild = transform.GetChild(0);
    }

    void Update()
    {
        // maybe switch to get axis raw?
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) > 0.0f || Mathf.Abs(vertical) > 0.0f)
        {
            movementVelocity.x = horizontal * Speed;
            movementVelocity.y = vertical * Speed;

            float dot = Vector2.Dot(movementVelocity, Vector2.up);
            float determinant = (Vector2.up.x * movementVelocity.y) - (Vector2.up.y * movementVelocity.x);
            float desiredSpriteAngle = Mathf.Atan2(determinant, dot) * Mathf.Rad2Deg;
            spriteAngle = Mathf.LerpAngle(spriteAngle, desiredSpriteAngle, Time.deltaTime * SpriteTurnSpeed);
            spriteChild.rotation = Quaternion.AngleAxis(spriteAngle, Vector3.forward);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleGrab(grabComponent == null);
        }
        CheckGrabComponents();
        DebugDraw();
    }

    void FixedUpdate()
    {
        Vector2 desiredPosition = (Vector2)(transform.position) + (movementVelocity * Time.fixedDeltaTime);
        controllerBody.MovePosition(desiredPosition);
        movementVelocity = Vector2.zero;
    }

    void CheckGrabComponents()
    {
        if( grabComponent )
        {
            grabComponent.Select();
        }
        else
        {
            var nearestGrabComponent = GetNearestGrabComponents();
            if( nearestGrabComponent )
            {
                nearestGrabComponent.Highlight();
            }
        }
    }

    //[Conditional("DEBUG")]
    void DebugDraw()
    {
#if UNITY_EDITOR
        DebugExtension.DrawCircle(new Vector2(transform.position.x, transform.position.y), controllerCollider.radius * GrabRadiusMultiplier, Color.green);
#endif
    }

    void HandleGrab(bool isGrabbing)
    {
        if (isGrabbing)
        {
            if (!grabComponent)
            {
                grabComponent = GetNearestGrabComponents();
                if (grabComponent)
                {
                    grabComponent.gameObject.transform.SetParent(transform);
                    return;
                }
            }
        }
        else
        {
            if (grabComponent)
            {
                grabComponent.gameObject.transform.SetParent(null);
                grabComponent = null;
            }
        }
    }


    GrabComponent GetNearestGrabComponents()
    {
        if (grabComponent)
            return grabComponent;
        else
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, controllerCollider.radius * GrabRadiusMultiplier);
            if (colliders.Length > 0)
            {
                // TODO pick the grab component that you are looking at (which might not be the first in the array)
                foreach (Collider2D collider in colliders)
                {
                    var nearestGrabComponent = collider.gameObject.GetComponent<GrabComponent>();
                    if (nearestGrabComponent)
                    {
                        return nearestGrabComponent;
                    }
                }
            }
        }
        return null;
    }
}
