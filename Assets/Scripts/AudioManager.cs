using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;
    void Awake()
    {
        foreach (Sounds sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
        }
    }
    public void Play(string name)
    {
        Sounds sound = Array.Find(sounds, sound => sound.name == name);
        sound.audioSource.Play();
    }
}
