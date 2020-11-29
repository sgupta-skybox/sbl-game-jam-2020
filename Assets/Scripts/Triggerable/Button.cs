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
    [SerializeField]
    List<GameObject> buttons;

    // Start is called before the first frame update
    void Start()
    {
        playerLayerMask = LayerMask.NameToLayer("Player");
        movableObjectLayerMask = LayerMask.NameToLayer("MovableObject");
        UpdateButtonState();
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
        TriggerObjects(true);
    }

    protected override void OnUntriggered()
    {
        TriggerObjects(false);
    }

    void TriggerObjects( bool isTriggered )
    {
        foreach (GameObject objectToTrigger in objectsToTrigger)
        {
            var door = objectToTrigger.GetComponent<Door>();
            if (door)
            {
                if (Toggles)
                    door.ToggleDoor();
                else
                    door.OpenDoor(isTriggered);
            }
            else
            {
                if (Toggles)
                {
                    objectToTrigger.SetActive(!objectToTrigger.activeSelf);
                }
                else
                {
                    objectToTrigger.SetActive(isTriggered);
                }
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
        UpdateButtonState();
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
        UpdateButtonState();
    }

    void UpdateButtonState()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (objectsOnTop.Count == 0)
            {
                buttons[i].SetActive(i == 0);
            }
            else
            {
                int idx = numBoxesToTrigger - objectsOnTop.Count + 1;
                buttons[i].SetActive(i == idx);
            }
        }
    }
}
