using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : TriggerableBase
{
	public GameObject triggerTarget;
	NPCTriggerManager triggerManager;
	public SpriteRenderer spriteRenderer;
	public List<TriggerableBase> triggerables;
	Color defaultColor;
	// Start is called before the first frame update
	void Start()
	{
		triggerManager = GetComponentInParent<NPCTriggerManager>();
		if( triggerManager)
		{
			triggerManager.RegisterTrigger(this);
		}
		defaultColor = spriteRenderer.color;
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if( collision.gameObject == triggerTarget)
		{
			if( triggerManager)
				triggerManager.Triggered();
			if( spriteRenderer )
				spriteRenderer.color = Color.white;
			triggerables.ForEach(trigger => trigger.IsTriggered = false);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject == triggerTarget)
		{
			if (spriteRenderer)
				spriteRenderer.color = defaultColor;
			triggerables.ForEach(trigger => trigger.IsTriggered = true);
			if (triggerManager)
				triggerManager.Untriggered();
		}
	}
	protected override void OnTriggered()
	{
		spriteRenderer.enabled = true;
		GetComponent<BoxCollider2D>().enabled = true;
	}

	protected override void OnUntriggered()
	{
		spriteRenderer.enabled = false;
		GetComponent<BoxCollider2D>().enabled = false;
	}
}
