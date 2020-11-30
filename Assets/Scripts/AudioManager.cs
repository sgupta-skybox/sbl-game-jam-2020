using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public const string NAME = "AudioManager";
    List<AudioClip> clipsPlayedThisFrame;
    AudioSource source;
    void Awake() // needs to happen before OnEnable is called on other scripts
    {
        DontDestroyOnLoad(gameObject);
        source = GetComponent<AudioSource>();
        clipsPlayedThisFrame = new List<AudioClip>();
    }

    void Update()
    {
        clipsPlayedThisFrame.Clear();
    }

    public void PlayClip(AudioClip audioClip, float volume = 1.0f)
    {
        if (!clipsPlayedThisFrame.Contains(audioClip))
        {
            clipsPlayedThisFrame.Add(audioClip);
            AudioSource.PlayClipAtPoint(audioClip,Vector3.zero, volume);
        }
    }

    public void PlayLoop(float volume = 0.2f)
    {
        source.volume = volume;
        source.Play();
    }
}
