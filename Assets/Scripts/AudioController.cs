using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {
    public ConfigLoader configLoader;
    private AudioClip clip;

    private void Start() {
        var config = configLoader.config;
        var fileName = config.name;
        clip = Resources.Load("Music/" + fileName) as AudioClip;
        Debug.Log("Clip Loaded");
    }

    public void PlayAudio() {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
}