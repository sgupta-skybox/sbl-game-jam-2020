using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredNPC : MonoBehaviour
{
	// Start is called before the first frame update\
	public bool alive = true;
	Color defaultColor;

	void Start()
	{
		defaultColor = GetComponent<SpriteRenderer>().color;
		if (!alive)
		{
			Die();
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	void Die()
	{		
		GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
		GetComponent<SpriteRenderer>().color = Color.grey;
		GetComponent<Mimic>().SetAlive( false );
		alive = false;
	}

	void Resurrect()
	{
		GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		GetComponent<SpriteRenderer>().color = defaultColor;
		GetComponent<Mimic>().enabled = true;
		GetComponent<Mimic>().SetAlive( true );
		alive = true;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Assasin" && alive)
		{
			Die();
		}

		else if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<Controller>() && !alive)
		{
			Resurrect();
		}
	}
}
