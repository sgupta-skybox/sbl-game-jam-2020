using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
	public GameObject triggerTarget;
	NPCTriggerManager triggerManager;

	public GameObject diedText;
	// Start is called before the first frame update
	void Start()
	{
		triggerManager = GetComponentInParent<NPCTriggerManager>();
		triggerManager.RegisterTrigger(this);
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
			GetComponent<Light>().enabled = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject == triggerTarget)
		{
			GetComponent<Light>().enabled = false;
			triggerManager.Untriggered();
		}
	}
}
