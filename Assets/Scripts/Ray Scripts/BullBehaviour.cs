using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BullBehaviour : MonoBehaviour
{
    private Vector3 destination = new Vector3(0, 0, 0);
    public NavMeshAgent agent;
    public Transform player;
    public int damage;
    public float aggroRange;
    public float attackCooldown;
    private float attackTimer;
    private bool charging;
    private Collider hitbox;
    void Start()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
        hitbox = GetComponent<Collider>();
    }

    void Update()
    {
        if((player.position - transform.position).magnitude <= aggroRange)
        {
            Follow();
        }
    }

    void Follow()
    {
        if(charging)
        {
            Charge();
            return;
        }
        if(attackCooldown <= attackTimer)
        {
            hitbox.isTrigger = enabled;
            charging = true;
            Vector3 vectorNorm = (player.position - transform.position).normalized;
            destination = player.position ;
            attackTimer = 0f;
            return;
        }
        hitbox.isTrigger = false;
        attackTimer += Time.deltaTime;
        transform.LookAt(player);
    }

    void Charge()
    {
        agent.destination = destination;
        if((destination - transform.position).magnitude <= 20f)
        {
            attackTimer = 0f;
            charging = false;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag.Equals("Player"))
        {
            Health hp = collider.GetComponent<Health>();
            hp.TakeDamage(damage);
        }
    }

}
