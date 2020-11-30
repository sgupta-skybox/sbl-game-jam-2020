using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotPotatoWinCondition : MonoBehaviour
{
    [SerializeField]
    Controller player;

    public GameObject[] gameObjects;
    public TriggerableBase doorTrigger;
    public AIBoxThrower enemy;

    bool[] triggeredObjects;
    // Start is called before the first frame update
    void Start()
    {
        triggeredObjects = new bool[gameObjects.Length];
        for(int i = 0; i < gameObjects.Length; ++i)
		{
            if (gameObjects[i].transform.position.x > 0)
                triggeredObjects[i] = true;
		}
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < gameObjects.Length; ++i)
        {
            if (gameObjects[i].transform.position.x > 0)
                triggeredObjects[i] = true;
        }
        int index = System.Array.FindIndex(gameObjects, gameObject=> gameObject == collision.gameObject);
        if( index >= 0)
		{
            triggeredObjects[index] = true;
        }

        if( System.Array.TrueForAll(triggeredObjects, triggeredObject => triggeredObject))
		{
            doorTrigger.IsTriggered = true;
            enemy.LevelFinish();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
	{
        int index = System.Array.FindIndex(gameObjects, gameObject => gameObject == collision.gameObject);
        if (index >= 0)
        {
            triggeredObjects[index] = false;
        }

        if(System.Array.TrueForAll(triggeredObjects, triggeredObject => !triggeredObject))
		{
            doorTrigger.IsTriggered = true;
            enemy.LevelFinish();
            player.Die();
        }
    }


}
