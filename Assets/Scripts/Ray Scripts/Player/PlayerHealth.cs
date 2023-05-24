using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    private int maxHealth;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public float invulnerabilityTime = 1f;
    public float flashingInterval = 0.1f;

    private float invulnerabilityTimer;
    private MeshRenderer spriteRenderer;

    void Start()
    {
        maxHealth = hp;
        UpdateHearts();
        spriteRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (invulnerabilityTimer > 0)
        {
            invulnerabilityTimer -= Time.deltaTime;
        }
    }

    override public void TakeDamage(int damage)
    {
        if (invulnerabilityTimer <= 0)
        {
            hp -= damage;
            invulnerabilityTimer = invulnerabilityTime;

            if (hp <= 0)
            {
                hp = 0;

                // Trigger player's death
                PlayerDeath playerDeath = GetComponent<PlayerDeath>();
                if (playerDeath != null)
                {
                    playerDeath.Die();
                }
            }

            UpdateHearts();
            StartCoroutine(FlashPlayer());
        }
    }

    private IEnumerator FlashPlayer()
    {
        float flashingTime = invulnerabilityTime;

        while (flashingTime > 0)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flashingInterval);
            flashingTime -= flashingInterval;
        }

        spriteRenderer.enabled = true;
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < hp)
            {
                hearts[i].sprite = fullHeart;
                hearts[i].gameObject.SetActive(true);
            }
            else
            {
                hearts[i].sprite = emptyHeart;
                hearts[i].gameObject.SetActive(false);
            }
        }
    }
}
