using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sounds
{
    public AudioClip clip;
    public string name;
    public float volume;
    public bool pitch;

    [HideInInspector]
    public AudioSource audioSource;
}
