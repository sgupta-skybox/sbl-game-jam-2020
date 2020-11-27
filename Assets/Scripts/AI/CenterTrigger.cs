using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterTrigger : MonoBehaviour
{

	enum CenterState
	{
		UnTriggered,
		Triggered,
		Activated,
		Completed
	}

	CenterState state;

	public SpriteRenderer untrigerred;
	public SpriteRenderer triggered;
	public SpriteRenderer activated;

	public List<Mimic> mimics;

	public GameObject Exit1;
	public GameObject Exit2;

	MeshRenderer diedText;

	// Start is called before the first frame update
	void Start()
	{
		diedText = GameObject.Find("GameOver").GetComponent<MeshRenderer>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Activate()
	{
		Debug.Assert(state == CenterState.Triggered);
		state = CenterState.Activated;
		triggered.enabled = false;
		activated.enabled = true;
		GetComponent<BoxCollider2D>().isTrigger = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (state == CenterState.UnTriggered )
		{
			if(collision.gameObject.tag == "Assasin")
			{
				diedText.enabled = true;
				return;
			}

			foreach (var mimic in mimics)
			{
				mimic.enabled = true;
			}
			untrigerred.enabled = false;
			state = CenterState.Triggered;
		}

		if( state == CenterState.Activated)
		{
			if (collision.gameObject.tag == "Assasin" && GameObject.FindGameObjectsWithTag("Player") == null)
			{
				diedText.enabled = true;
				return;
			}

			if( collision.gameObject.tag == "Player")
			{
				GetComponent<Light>().enabled = true;
				Exit1.SetActive(false);
				Exit2.SetActive(false);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (state == CenterState.Triggered)
		{
			triggered.enabled = true;
			GetComponent<BoxCollider2D>().isTrigger = false;
		}

		if (state == CenterState.Activated)
		{
			GetComponent<Light>().enabled = false;
			Exit1.SetActive(true);
			Exit2.SetActive(true);
		}
	}

}
