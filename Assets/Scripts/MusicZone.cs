using System.Collections;
using UnityEngine;

public class MusicZone : MonoBehaviour
{
    public AudioClip musicClip;
    public float zoneRadius = 5f;  // set the default radius
    private AudioSource _audioSource;
    private SphereCollider _sphereCollider;
    public float fadeTime = 1f;  // adjust as needed for longer/shorter fade

    void Start()
    {
        _sphereCollider = gameObject.AddComponent<SphereCollider>();
        _sphereCollider.radius = zoneRadius;
        _sphereCollider.isTrigger = true;

        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.clip = musicClip;
        _audioSource.volume = 0f;  // initially silent
        _audioSource.loop = true;
        _audioSource.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopAllCoroutines();  // stop any ongoing fade
            StartCoroutine(FadeIn(fadeTime));  // fade in
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopAllCoroutines();  // stop any ongoing fade
            StartCoroutine(FadeOut(fadeTime));  // fade out
        }
    }

    IEnumerator FadeIn(float fadeTime)
    {
        while (_audioSource.volume < 1.0f)
        {
            _audioSource.volume += Time.deltaTime / fadeTime;

            yield return null;
        }

        _audioSource.volume = 1f;  // ensure volume is exactly 1.0 at end of fade
    }

    IEnumerator FadeOut(float fadeTime)
    {
        while (_audioSource.volume > 0.0f)
        {
            _audioSource.volume -= Time.deltaTime / fadeTime;

            yield return null;
        }

        _audioSource.volume = 0f;  // ensure volume is exactly 0.0 at end of fade
    }

    void OnDrawGizmosSelected()
{
    if (_sphereCollider != null)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _sphereCollider.radius);
    }
}


    // Update the collider radius when changed in the inspector
    void OnValidate()
    {
        if (_sphereCollider != null)
        {
            _sphereCollider.radius = zoneRadius;
        }
    }
}
