using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFailCheck : MonoBehaviour
{
    [SerializeField]
    public GameObject Door;

    private int playerLayer;
    private int boxLayer;

    private int numBoxesInTrigger = 0;
    private bool playerInside = false;

    // Start is called before the first frame update
    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        boxLayer = LayerMask.NameToLayer("MovableObject");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInside && numBoxesInTrigger < 3 && Door.activeSelf)
        {
            GameObject.Find("Player").GetComponent<Controller>().Invoke("Die", 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == boxLayer)
        {
            ++numBoxesInTrigger;
        }
        if (other.gameObject.layer == playerLayer)
        {
            playerInside = true;
            print("player inside!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == boxLayer)
        {
            --numBoxesInTrigger;
        }
        if (other.gameObject.layer == playerLayer)
        {
            playerInside = false;
            print("player outside!");
        }
    }
}
