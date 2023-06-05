using System.Collections;
using UnityEngine;

public class MusicZone : MonoBehaviour
{
    public AudioClip musicClip;
    public float zoneRadius = 5f;  // set the default radius
    private AudioSource _audioSource;
    private SphereCollider _sphereCollider;
    private bool _isPlayerInside = false;
    
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
            _isPlayerInside = true;
            _audioSource.volume = 1f;  // becomes audible
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _isPlayerInside = false;
            _audioSource.volume = 0f;  // becomes silent
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _sphereCollider.radius);
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

