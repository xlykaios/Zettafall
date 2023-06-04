using System.Collections;
using UnityEngine;

public class MusicZone : MonoBehaviour
{
    public float radius = 5f; // The range of the music zone. Set this in the Inspector.
    public float fadeTime = 1f; // Time it takes for the music to fade in and out. Set this in the Inspector.
    public AudioClip musicClip; // AudioClip to play. Set this in the Inspector.
    public Color gizmoColor = Color.yellow; // Color of the gizmo in the Scene view. Set this in the Inspector.

    private AudioSource audioSource;
    private SphereCollider sphereCollider;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.playOnAwake = false;
        audioSource.volume = 0f;
        
        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = radius;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(FadeAudioToFullVolume(fadeTime));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(FadeAudioToZeroVolume(fadeTime));
        }
    }

    IEnumerator FadeAudioToFullVolume(float time)
    {
        float startVolume = 0;

        while (audioSource.volume < 0)
        {
            audioSource.volume += startVolume * Time.deltaTime / time;
            yield return null;
        }

        audioSource.Play();
    }

    IEnumerator FadeAudioToZeroVolume(float time)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / time;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}

