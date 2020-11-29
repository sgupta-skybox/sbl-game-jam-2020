using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDetector : MonoBehaviour
{
    [SerializeField]
    public int teamId;
    public int numBoxes = 0;

    [NonSerialized]
    public Vector2 BoxSize;
    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        BoxSize = boxCollider.size;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.CompareTag("Throwable") )
        {
            numBoxes += 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Throwable"))
        {
            numBoxes -= 1;
        }
    }
}
