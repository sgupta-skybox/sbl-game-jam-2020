using UnityEngine;

public enum Direction
{
	Up,
	Down,
	Left,
	Right
}

public class Mimic : MonoBehaviour
{
	public Direction upMimicDirection = Direction.Up;
	public Direction downMimicDirection = Direction.Down;
	public Direction rightMimicDirection = Direction.Right;
	public Direction leftMimicDirection = Direction.Left;

	public float speedModifier = 1.0f;

	public GameObject mimicTarget;

	private Transform mimicTransform;
	private Vector3 mimicPreviousPosition;

	private Transform myTransform;

	// Start is called before the first frame update
	void Start()
	{
		if (upMimicDirection == leftMimicDirection ||
			upMimicDirection == rightMimicDirection ||
			upMimicDirection == downMimicDirection ||
			leftMimicDirection == rightMimicDirection ||
			leftMimicDirection == downMimicDirection ||
			rightMimicDirection == downMimicDirection )
		{
			Debug.LogWarning("Mimic should have all directions specified!");
			return;
		}

		myTransform = GetComponent<Transform>();
		if( transform == null )
		{
			Debug.LogWarning("Any object with Mimic script should have a transform component!");
			return;
		}

		if (mimicTarget == null)
		{
			Debug.LogWarning("Attached Mimic Script is missing the mimic target");
			return;
		}

		mimicTransform = mimicTarget.GetComponent<Transform>();

		if (!mimicTransform)
		{
			Debug.LogWarning("Attached Mimic target should have a transform component!");
			return;
		}

		mimicPreviousPosition = mimicTransform.position;
	}

	void ComputeMimicValue(Direction direction, float movement, ref Vector3 targetMovement)
	{
		switch( direction )
		{
			case Direction.Up: targetMovement.y = movement;
				break;
			case Direction.Down: targetMovement.y = -movement;
				break;
			case Direction.Left: targetMovement.x = -movement;
				break;
			case Direction.Right: targetMovement.x = movement;
				break;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (!mimicTransform)
			return;

		Vector3 deltaPosition = mimicTransform.position - mimicPreviousPosition;
		mimicPreviousPosition = mimicTransform.position;

		Vector3 targetDeltaPosition = new Vector3();
		if( deltaPosition.x != 0)
		{
			ComputeMimicValue(deltaPosition.x > 0 ? rightMimicDirection : leftMimicDirection, Mathf.Abs(deltaPosition.x), ref targetDeltaPosition);
		}

		if( deltaPosition.y != 0)
		{
			ComputeMimicValue(deltaPosition.y > 0 ? upMimicDirection : downMimicDirection, Mathf.Abs(deltaPosition.y), ref targetDeltaPosition);
		}

		myTransform.position += targetDeltaPosition * speedModifier;
	}
}
