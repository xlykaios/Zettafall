using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BruiserBehaviour : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform player;

    public float attackCooldown;
    private float attackTimer;

    public float aggroRange;

    public float distanceKeep;
    //Defensive
    //keep a certain distance from the player
    public GameObject attackPrefab;
    // Start is called before the first frame update
    void Start()
    {
        var cubeRenderer = gameObject.GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_Color", Color.black);
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToPoint = player.position - transform.position;
        if (distanceToPoint.magnitude <= aggroRange)
        {
            Defensive();
        } else {
            Guarding();
        }

    }

    //Guarding
    private void Guarding()
    {
        //plays the idle animation
    }

    private void Defensive()
    {
        var cubeRenderer = gameObject.GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_Color", Color.black);
        attackTimer += Time.deltaTime;
        //get to a position at a set distance from the player
        if (player.position.x < transform.position.x)
        {
            agent.destination = new Vector3(player.position.x + distanceKeep, transform.position.y, player.position.z);
        }
        if (player.position.x > transform.position.x)
        {
            agent.destination = new Vector3(player.position.x - distanceKeep, transform.position.y, player.position.z);
        }

        if(attackTimer >= attackCooldown)
        {
            attackTimer = 0f;
            Attack();
        }


    }

    private void Attack()
    {
        Instantiate(attackPrefab, transform.position, Quaternion.identity);
    }


}
