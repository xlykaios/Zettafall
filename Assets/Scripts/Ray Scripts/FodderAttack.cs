using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FodderAttack : MonoBehaviour
{
    public int damage;
    public float attackDuration;
    private float timer;
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.GetComponent<Health>() != null && collider.gameObject.tag.Equals("Player"))
        {
            Health health = collider.GetComponent<Health>();
            health.TakeDamage(damage);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= attackDuration)
        {
            Destroy(gameObject);
        }
    }

}
