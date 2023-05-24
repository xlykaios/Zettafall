using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int hp;
    public int hpMax;
    // Start is called before the first frame update
    void Start()
    {
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
    }

    virtual public void HealHP(int health)
    {
        if(hp < hpMax)
        {
            hp += health;
        }
    }

}
