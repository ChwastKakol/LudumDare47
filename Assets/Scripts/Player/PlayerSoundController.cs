using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour {
    public AudioSource _audioSource;
    public AudioClip walkingSound;
    
    private void Awake() {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.clip = walkingSound;
        _audioSource.loop = true;
        _audioSource.playOnAwake = false;
        _audioSource.Stop();
    }

    public void StartWalking() {
        _audioSource.Play();
    }

    public void StopWalking() {
        _audioSource.Stop();
    }
}
