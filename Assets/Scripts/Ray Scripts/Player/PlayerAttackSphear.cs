using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSphear : MonoBehaviour
{
    public GameObject sphearPrefab;
    public KeyCode attackKey;
    public float attackCooldown;
    public AudioClip sphearSound; // AudioClip to play. Set this in the Inspector.
    private AudioSource audioSource; // AudioSource component
    private float attackTimer;
    private bool sphearReady;

    // Start is called before the first frame update
    void Start()
    {
        sphearReady = true;
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    // Update is called once per frame
    void Update()
    {
        if (sphearReady)
        {
            attackTimer += Time.deltaTime;
        }
        if (Input.GetKeyDown(attackKey))
        {
            Attack();
        }
    }
    private void Attack()
    {
        if (!sphearReady)
        {
            return;
        }
        if (attackTimer >= attackCooldown)
        {
            attackTimer = 0f;
            TossSphear();
        }
    }
    private void TossSphear()
    {
        Instantiate(sphearPrefab, GameObject.Find("AttackCenter").GetComponent<Transform>().position, Quaternion.identity);
        audioSource.PlayOneShot(sphearSound); // Play the sphear sound
    }

    public void ToggleSphear()
    {
        sphearReady = !sphearReady;
    }

}

