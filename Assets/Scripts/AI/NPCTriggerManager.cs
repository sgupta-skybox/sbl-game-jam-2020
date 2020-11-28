using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTriggerManager : MonoBehaviour
{
    List<NPCTrigger> childTriggers = new List<NPCTrigger>();
    int numTriggered = 0;

    public List<TriggerableBase> triggerables;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RegisterTrigger(NPCTrigger trigger)
	{
        childTriggers.Add(trigger);
	}

    public void Triggered()
	{
        ++numTriggered;
        if(childTriggers.Count == numTriggered)
		{
            foreach( var trigger in childTriggers)
			{
                trigger.IsTriggered = false;
            }

            triggerables.ForEach(trigger => trigger.IsTriggered = true);
        }
	}

    public void Untriggered()
    {
        --numTriggered;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
