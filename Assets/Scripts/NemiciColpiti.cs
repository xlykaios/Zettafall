using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NemiciColpiti : MonoBehaviour
{
    public AudioClip hitSound; // AudioClip to play when hit. Set this in the Inspector.
    public float hitSoundVolume = 1f; // Default volume is max
    private AudioSource audioSource; // AudioSource component

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        // Set the initial volume
        audioSource.volume = hitSoundVolume;
    }

    // Method to play the hit sound
    public void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }
}
