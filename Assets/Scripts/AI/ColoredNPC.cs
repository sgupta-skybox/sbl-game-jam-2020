using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredNPC : MonoBehaviour
{
	// Start is called before the first frame update
	MeshRenderer diedText;

	void Start()
	{
		diedText = GameObject.Find("GameOver").GetComponent<MeshRenderer>();
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

		if (collision.gameObject.GetComponent<Mimic>() && gameObject.tag != "Assasin")
			Die();

		if ( collision.gameObject.tag == "Player")
		{
			Destroy(collision.gameObject.GetComponent<PhysicsMovement>());
			collision.gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
			if( gameObject.tag == "Assasin")
			{
				gameObject.AddComponent<PhysicsMovement>();
				Destroy(gameObject.GetComponent<Mimic>());
			}
			else
			{
				diedText.enabled = true;
			}
		}
	}
}
