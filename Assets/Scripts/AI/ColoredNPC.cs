using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredNPC : MonoBehaviour
{
	// Start is called before the first frame update

	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{

	}

	void Die()
	{		
		GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
		GetComponent<SpriteRenderer>().color = Color.grey;
		Destroy(GetComponent<Mimic>());
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!GetComponent<Mimic>())
			return;

		else if (collision.gameObject.tag == "Assasin")
		{
			Die();
		}

		else if ( collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Controller>())
		{
			if( gameObject.tag == "Assasin")
			{
				gameObject.GetComponent<Controller>().enabled = true;
				Destroy(collision.gameObject.GetComponent<Controller>());
				collision.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.grey;
				Destroy(gameObject.GetComponent<Mimic>());
			}
		}
	}
}
