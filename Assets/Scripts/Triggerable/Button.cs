using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : TriggerableBase
{
    [SerializeField]
    public GameObject objectToTrigger;
    public int numBoxesToTrigger;
    public float timer;

    private int playerLayerMask = 0;
    private int movableObjectLayerMask = 0;

    private float currentTimer;
    private bool timerStarted = false;

    private  List<GameObject> objectsOnTop = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        playerLayerMask = LayerMask.NameToLayer("Player");
        movableObjectLayerMask = LayerMask.NameToLayer("MovableObject");
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStarted)
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0)
            {
                timerStarted = false;
                IsTriggered = false;
            }
        }
    }

    protected override void OnTriggered() 
    {
        currentTimer = timer;
        objectToTrigger.SetActive(false);
    }

    protected override void OnUntriggered()
    {
        objectToTrigger.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayerMask || other.gameObject.layer == movableObjectLayerMask)
        {
            // don't add same object again
            if (objectsOnTop.Find(obj => obj.name == other.name))
            {
                return;
            }

            objectsOnTop.Add(other.gameObject);
            if (objectsOnTop.Count >= numBoxesToTrigger)
            {
                IsTriggered = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayerMask || other.gameObject.layer == movableObjectLayerMask)
        {
            objectsOnTop.Remove(other.gameObject);
            if (objectsOnTop.Count < numBoxesToTrigger)
            {
                timerStarted = true;
            }
        }
    }
}
