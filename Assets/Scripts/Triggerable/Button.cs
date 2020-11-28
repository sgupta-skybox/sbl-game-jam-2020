using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : TriggerableBase
{
    [SerializeField]
    public List<GameObject> objectsToTrigger;
    public int numBoxesToTrigger;
    public float timer;
    public bool playerCounts = true;
    public bool boxCounts = true;
    public bool Toggles = true;

    private int playerLayerMask = 0;
    private int movableObjectLayerMask = 0;

    private float currentTimer;
    private bool timerStarted = false;

    private List<GameObject> objectsOnTop = new List<GameObject>();

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
        foreach (GameObject objectToTrigger in objectsToTrigger)
        {
            if (Toggles)
            {
                objectToTrigger.SetActive(!objectToTrigger.activeSelf);
            }
            else
            {
                objectToTrigger.SetActive(false);
            }
        }
    }

    protected override void OnUntriggered()
    {
        foreach (GameObject objectToTrigger in objectsToTrigger)
        {
            if (Toggles)
            {
                objectToTrigger.SetActive(!objectToTrigger.activeSelf);
            }
            else
            {
                objectToTrigger.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerCounts && other.gameObject.layer == playerLayerMask)
        {
            OnTriggerEnterLogic(other);
        }
        else if (boxCounts && other.gameObject.layer == movableObjectLayerMask)
        {
            OnTriggerEnterLogic(other);
        }
    }

    private void OnTriggerEnterLogic(Collider2D other)
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
            timerStarted = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ((playerCounts && other.gameObject.layer == playerLayerMask) ||
            (boxCounts && other.gameObject.layer == movableObjectLayerMask))
        {
            OnTriggerExitLogic(other);
        }
    }

    private void OnTriggerExitLogic(Collider2D other)
    {
        if (objectsOnTop.Remove(other.gameObject))
        {
            if (objectsOnTop.Count < numBoxesToTrigger)
            {
                timerStarted = true;
            }
        }        
    }
}
