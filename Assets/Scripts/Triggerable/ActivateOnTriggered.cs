using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnTriggered : TriggerableBase
{
    public bool flipActivation = false;
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
        gameObject.SetActive(!flipActivation);
    }

    protected override void OnUntriggered()
    {
        gameObject.SetActive(flipActivation);
    }
}
