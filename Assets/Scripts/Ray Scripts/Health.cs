using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hp;
    public int hpMax;

    // Get reference to NemiciColpiti
    private NemiciColpiti nemiciColpiti;

    // Start is called before the first frame update
    void Start()
    {
        // Find the NemiciColpiti script on the same GameObject
        nemiciColpiti = GetComponent<NemiciColpiti>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    virtual public void TakeDamage(int damage)
    {
        hp -= damage;

        // Play hit sound if the enemy takes damage
        if(nemiciColpiti != null)
        {
            nemiciColpiti.PlayHitSound();
        }
    }

    virtual public void HealHP(int health)
    {
        if(hp < hpMax)
        {
            hp += health;
        }
    }
}
