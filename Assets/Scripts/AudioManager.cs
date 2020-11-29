using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public const string NAME = "AudioManager";
    List<AudioClip> clipsPlayedThisFrame;
    void Awake() // needs to happen before OnEnable is called on other scripts
    {
        clipsPlayedThisFrame = new List<AudioClip>();
    }

    void Update()
    {
        clipsPlayedThisFrame.Clear();
    }

    public void PlayClip(AudioClip audioClip)
    {
        if (!clipsPlayedThisFrame.Contains(audioClip))
        {
            clipsPlayedThisFrame.Add(audioClip);
            AudioSource.PlayClipAtPoint(audioClip, Vector2.zero);
        }
    }
}
