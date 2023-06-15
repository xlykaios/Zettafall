using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruiserHealth : Health
{
    

    //Invulnerable
    public int shield;
    private int shieldConst;
    private bool isInvulnerable;
    //Vulnerable
    public float vulnerabilityPeriod;
    private float vulnTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        shieldConst = shield;
    }

    // Update is called once per frame
    void Update()
    {
        if(shield > 0)
        {
            isInvulnerable = true;
        }
        if(shield <= 0)
        {
            vulnTimer += Time.deltaTime;
            isInvulnerable = false;
            if(vulnTimer >= vulnerabilityPeriod)
            {
                isInvulnerable = true;
                vulnTimer = 0f;
                shield = shieldConst;
            }
        }
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    override public void TakeDamage(int damage)
    {
        if(isInvulnerable)
        {
            shield -= damage;
        }
        if(!isInvulnerable)
        {
            hp -= damage;
            Instantiate(HitVFX, gameObject.transform.position, gameObject.transform.rotation);

        }
    }

}
