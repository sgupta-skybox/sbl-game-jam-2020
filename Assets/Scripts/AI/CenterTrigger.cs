using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterTrigger : MonoBehaviour
{
	public List<TriggerableBase> triggerables;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		triggerables.ForEach(trigger => trigger.IsTriggered = true);
		Destroy(gameObject);
	}

}
