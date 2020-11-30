using System.Collections;
using UnityEngine;

public class AIBoxThrower : Controller
{
    ThrowComponent targetComp;
    [SerializeField]
    BoxDetector field;

    
    [SerializeField]
    BoxDetector oppField;
    Vector2 targetThorwingPos;

    public int cooldownAfterGrab = 300;
    public float cooldownAfterThrow = 1.5f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        isPlayable = false;
        StartCoroutine(MakeDecision());
    }

    // Update is called once per frame
    IEnumerator MakeDecision()
    {
        while (true)
        {
            if (targetComp == null && grabComponent == null)
            {
                targetComp = FindClosestComponent();
            }
            else if(grabComponent == null)
            {
                yield return TryGrabClosestComponent();
            }
            else
            {
                yield return TryThrowBoxToTheOpponent();
            }
            yield return null;
        }
    }

    ThrowComponent FindClosestComponent()
    {
        ThrowComponent targetBox = null;
        int targetLayer = 1 << LayerMask.NameToLayer("MovableObject");
        var centerPt = field.transform.TransformPoint(field.transform.position);
        var colliders = Physics2D.OverlapBoxAll(field.transform.position, field.BoxSize, 360.0f, targetLayer);
        if (colliders != null && colliders.Length > 0 )
        {
            float closestDistSq = float.MaxValue;
            foreach( var collider in colliders )
            {
                var box = collider.GetComponent<ThrowComponent>();
                if ( box != null && box.transform.position.x > 0)
                {
                    var distSq = (box.transform.position - transform.position).sqrMagnitude;
                    if(distSq < closestDistSq )
                    {
                        closestDistSq = distSq;
                        targetBox = box;
                    }
                }
            }
        }
        return targetBox;
    }

    IEnumerator TryGrabClosestComponent()
    {
        while(grabComponent == null)
        {
            if (targetComp.transform.position.x < 0)
            {
                break;
            }

            var direction = targetComp.transform.position - transform.position;
            movementVelocity = new Vector2(direction.x, direction.y).normalized * Speed;
            var comp = GetNearestGrabComponents();
            if(comp != null )
            {
                HandleGrab(true);
            }
            yield return null;
        }
        targetComp = null;
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator TryThrowBoxToTheOpponent()
    {
        var x = Random.Range(oppField.transform.position.x - oppField.BoxSize.x * 0.3f, oppField.transform.position.x + oppField.BoxSize.y * 0.3f);
        var y = Random.Range(oppField.transform.position.y - oppField.BoxSize.y * 0.3f, oppField.transform.position.y + oppField.BoxSize.y * 0.3f);
        while (grabComponent != null)
        {
            int tick = 0;
            while (tick++ < cooldownAfterGrab)
            {
                var dx = x - transform.position.x;
                var dy = y - transform.position.y;
                movementVelocity = new Vector2(dx, dy).normalized * Speed;
                yield return null;
            }
            HandleThrow();
            yield return null;
        }
        yield return new WaitForSeconds(cooldownAfterThrow);
    }

    public void LevelFinish()
	{
        StopAllCoroutines();
	}
}
