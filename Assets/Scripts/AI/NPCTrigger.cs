using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
	public GameObject triggerTarget;
	NPCTriggerManager triggerManager;
	public GameObject centerTrigger;

	Color defaultColor;
	// Start is called before the first frame update
	void Start()
	{
		triggerManager = GetComponentInParent<NPCTriggerManager>();
		triggerManager.RegisterTrigger(this);
		defaultColor = GetComponent<SpriteRenderer>().color;
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if( collision.gameObject == triggerTarget)
		{
			triggerManager.Triggered();
			GetComponent<SpriteRenderer>().color = Color.white;
			centerTrigger.gameObject.SetActive( false );
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject == triggerTarget)
		{
			GetComponent<SpriteRenderer>().color = defaultColor;
			centerTrigger.gameObject.SetActive(true);
			triggerManager.Untriggered();
		}
	}
}
