using UnityEngine;

public class PlayerMovementSound : MonoBehaviour
{
    public AudioClip walkClip; // AudioClip to play. Set this in the Inspector.
    private AudioSource audioSource; // AudioSource component
    private Vector3 lastPos; // Store the last position of the player
    public float walkSoundDelay = 0.5f; // Delay between walk sounds in seconds. Set this in the Inspector.
    private float nextWalkSoundTime = 0; // Time when the next walk sound can play

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        lastPos = transform.position; // Initialize lastPos
    }

    void Update()
    {
        // Check if the player is moving
        bool currentlyWalking = (transform.position != lastPos);

        // If the player is walking and enough time has passed since the last walk sound...
        if (currentlyWalking && Time.time > nextWalkSoundTime)
        {
            audioSource.PlayOneShot(walkClip);
            nextWalkSoundTime = Time.time + walkSoundDelay; // Set the time for the next walk sound
        }

        lastPos = transform.position; // Update lastPos for the next frame
    }
}

