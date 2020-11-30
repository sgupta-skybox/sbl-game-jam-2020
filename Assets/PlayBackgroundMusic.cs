using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBackgroundMusic : MonoBehaviour
{
    void OnTriggerEnter2D()
    {
        GameObject.FindGameObjectWithTag(AudioManager.NAME).GetComponent<AudioManager>().PlayLoop();
    }
}
