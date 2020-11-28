using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCleanTheWorld : MonoBehaviour
{
    [SerializeField]
    Controller player;

    [SerializeField]
    List<BoxDetector> fields;

    // Update is called once per frame
    void Update()
    {
        foreach( var field in fields )
        {
            if( field.numBoxes == 0 )
            {
                if( field.teamId == 0)
                {
                    player.Die();
                }
                else
                {
                    //player.Win();
                }
            }
        }
    }
}
