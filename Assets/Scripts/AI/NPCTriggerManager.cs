using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTriggerManager : MonoBehaviour
{
    List<NPCTrigger> childTriggers = new List<NPCTrigger>();


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
        bool allTriggered = true;
        foreach (var trigger in childTriggers)
        {
            allTriggered &= trigger.npcBoxTriggered;
        }

        if(allTriggered)
		{
            foreach( var trigger in childTriggers)
			{
                trigger.IsTriggered = false;
            }

            triggerables.ForEach(trigger => trigger.IsTriggered = true);
        }
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
