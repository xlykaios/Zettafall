using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public GameObject paperExplosionPrefab;
    private bool isDead = false;

    private void Update()
    {
        if (isDead)
        {
            // Add any additional behavior when the player is dead, if necessary
        }
    }

    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            // Instantiate the paper explosion effect at the player's position
            GameObject explosion = Instantiate(paperExplosionPrefab, transform.position, Quaternion.identity);
            // Optionally, destroy the explosion object after a few seconds
            Destroy(explosion, 5f);

            // Disable player movement and other related scripts
            GetComponent<PlayerMovement>().enabled = false;
            // Add other scripts to disable if necessary

            // Destroy the player after some delay (if necessary)
            // Destroy(gameObject, delay);
        }
    }
}

