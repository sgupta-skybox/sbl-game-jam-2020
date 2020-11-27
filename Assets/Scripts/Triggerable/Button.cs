using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : TriggerableBase
{
    [SerializeField]
    public GameObject objectToTrigger;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnTriggered() 
    {
        objectToTrigger.SetActive(false);
    }

    protected override void OnUntriggered()
    {
        objectToTrigger.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")
            || other.gameObject.layer == LayerMask.NameToLayer("MovableObject"))
        {
            IsTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")
            || other.gameObject.layer == LayerMask.NameToLayer("MovableObject"))
        {
            IsTriggered = false;
        }
    }
}
