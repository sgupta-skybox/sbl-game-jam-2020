using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTriggerManager : MonoBehaviour
{
    List<NPCTrigger> triggers = new List<NPCTrigger>();
    int numTriggered = 0;

    public CenterTrigger centerTrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RegisterTrigger(NPCTrigger trigger)
	{
        triggers.Add(trigger);
	}

    public void Triggered()
	{
        ++numTriggered;
        if( triggers.Count == numTriggered)
		{
            foreach( var npcTrigger in triggers)
			{
                Destroy(npcTrigger.gameObject.GetComponent<Light>());
                Destroy(npcTrigger.gameObject.GetComponent<Collider2D>());
            }
            centerTrigger.Activate();
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
