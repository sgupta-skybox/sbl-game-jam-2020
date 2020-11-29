using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]
    protected float Speed = 10.0f;
    public float deathTimer;

    [SerializeField]
    [Range(1.0f, 2.0f)]
    float GrabRadiusMultiplier = 1.2f;

    public Vector2 movementVelocity;

    protected GrabComponent grabComponent;
    CircleCollider2D controllerCollider;
    Rigidbody2D controllerBody;

    Transform spriteChild;
    float spriteAngle = 0.0f;

    MeshRenderer diedText;

    Animator animator;

    [SerializeField]
    float SpriteTurnSpeed = 5.0f;

    protected bool isPlayable = true;

    public Action<Controller> OnGrabReleased;

    public bool facingRight = true;

    AudioManager audioManager;

    [SerializeField]
    AudioClip GrabSound;

    [SerializeField]
    AudioClip ThrowSound;

    public float ColliderRadius
    {
        get { return controllerCollider != null ? controllerCollider.radius * GrabRadiusMultiplier : 0.0f; }
    }


    protected virtual void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag(AudioManager.NAME).GetComponent<AudioManager>();

        movementVelocity = Vector2.zero;
        controllerCollider = GetComponent<CircleCollider2D>();
        controllerBody = GetComponent<Rigidbody2D>();
        spriteChild = transform.GetChild(0);
        GameObject gameOver = GameObject.Find("GameOver");
        if (gameOver)
        {
            diedText = gameOver.GetComponent<MeshRenderer>();
        }

        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // maybe switch to get axis raw?
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        animator.SetFloat("HorizontalSpeed", Mathf.Abs(horizontal));
        animator.SetFloat("VerticalSpeed", Mathf.Abs(vertical));

        if (isPlayable)
        {
            if (Mathf.Abs(horizontal) > 0.0f || Mathf.Abs(vertical) > 0.0f)
            {
                movementVelocity.x = horizontal * Speed;
                movementVelocity.y = vertical * Speed;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                HandleGrab(grabComponent == null);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                HandleThrow();
            }
        }

        CheckGrabComponents();
        DebugDraw();
    }

    void FixedUpdate()
    {
        Vector2 desiredPosition = (Vector2)(transform.position) + (movementVelocity * Time.fixedDeltaTime);
        controllerBody.MovePosition(desiredPosition);
        if( facingRight ^ (movementVelocity.x > 0))
		{
            Flip();
		}

        movementVelocity = Vector2.zero;
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriteChild = transform.GetChild(0);
        Vector3 Scaler = spriteChild.localScale;
        Scaler.x *= -1;
        spriteChild.localScale = Scaler;
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

    protected void HandleGrab(bool tryGrabbing)
    {
        if (tryGrabbing)
        {
            if (!grabComponent)
            {
                grabComponent = GetNearestGrabComponents();
                if (grabComponent)
                {
                    if (GrabSound)
                    {
                        audioManager.PlayClip(GrabSound);
                    }
                    grabComponent.gameObject.transform.SetParent(transform);
                    ThrowComponent throwComponent = grabComponent.gameObject.GetComponent<ThrowComponent>();
                    if (throwComponent)
                    {
                        throwComponent.OnGrabbed(this);
                    }
                    return;
                }
            }
        }
        else
        {
            if (grabComponent)
            {
                Vector2 position = grabComponent.gameObject.transform.position;
                grabComponent.gameObject.transform.SetParent(null);
                grabComponent.transform.position = position;
                grabComponent.transform.localPosition = position;
                grabComponent = null;
                OnGrabReleased?.Invoke(this);
            }
        }
    }

    protected void HandleThrow()
    {
        if (grabComponent)
        {
            if (ThrowSound)
            {
                audioManager.PlayClip(ThrowSound);
            }
            grabComponent.gameObject.transform.SetParent(null);
            ThrowComponent throwComponent = grabComponent.gameObject.GetComponent<ThrowComponent>();
            if (throwComponent)
            {
                Vector2 throwDirection = (throwComponent.transform.position - transform.position).normalized;
                throwComponent.Throw(throwDirection);
                OnGrabReleased?.Invoke(this);
            }
            grabComponent = null;
        }
    }


    protected GrabComponent GetNearestGrabComponents()
    {
        if (grabComponent)
            return grabComponent;
        else
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, controllerCollider.radius * GrabRadiusMultiplier);
            if (colliders.Length > 0)
            {
                float closestDistSq = float.MaxValue;
                GrabComponent closestComp = null;
                // TODO pick the grab component that you are looking at (which might not be the first in the array)
                foreach (Collider2D collider in colliders)
                {
                    var grabComponent = collider.gameObject.GetComponent<GrabComponent>();
                    if (grabComponent)
                    {
                        float distSq = (transform.position - grabComponent.transform.position).sqrMagnitude;
                        if (distSq < closestDistSq)
                        {
                            closestDistSq = distSq;
                            closestComp = grabComponent;
                        }
                        else if(distSq.IsClosed(closestDistSq))
                        {
                            Vector3 forward = transform.TransformDirection(movementVelocity);
                            Vector3 dirGrabComp = (grabComponent.transform.position - transform.position).normalized;
                            Vector3 dirClosestGrabComp = (closestComp.transform.position - transform.position).normalized;
                            float angleBetween = Vector3.Dot(forward, dirGrabComp);
                            if (angleBetween > Vector3.Dot(forward, dirClosestGrabComp))
                            {
                                closestComp = grabComponent;
                            }
                        }
                    }
                }
                return closestComp;
            }
        }
        return null;
    }

    IEnumerator WaitAndRestart()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(deathTimer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Die()
	{
        if (diedText)
        {
            diedText.enabled = true;
        }
        Speed = 0;
        animator.SetTrigger("Death");
        GetComponentInChildren<SpriteRenderer>().color = Color.grey;
        StartCoroutine(WaitAndRestart());
    }
}
