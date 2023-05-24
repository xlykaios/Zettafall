using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FodderBehaviour : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    //Wander
    //Using a range of distance and time, moves from a point to another 
    public float wanderingArea;
    public Vector3 wanderDestination;
    public bool reachedWanderDestination;
    public float minWanderTime, maxWanderTime;
    private float randomWanderTime;
    private float wanderTime;
    public float aggroRange;
    //Shy
    public float maxDisToPlayer;
    public float minShyTime, maxShyTime;
    private float randomShyTime;
    private bool isAware;
    private float shyTime;

    //Aggressive
    public float attackCooldown;
    private float attackTimer; 
    private bool isAggroed = false;
    private float changeDistance;
    public float attackRange;
    private Vector3 chasePlayer;
    public GameObject attackPrefab;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PlayerObj").transform;
        randomShyTime = Random.Range(minShyTime, maxShyTime);
        reachedWanderDestination = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToPoint = transform.position - wanderDestination;
        if (distanceToPoint.magnitude < 1f)
        {
            reachedWanderDestination = true;
        }
        Vector3 distanceToPlayer = player.position - transform.position;
        
        if (distanceToPlayer.magnitude < aggroRange)
        {
            isAware = true;
        } else {
            isAware = false;
            isAggroed = false;
        }
        if(!isAggroed && !isAware)
        {
            Wandering();
        }
        if(!isAggroed && isAware)
        {
            Shy();
        }
        if(isAggroed && isAware)
        {
            Aggressive();
        }

    }

    //wandering
    //a random point where to walk to
    private void Wandering()
    {
        var cubeRenderer = gameObject.GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_Color", Color.green);
        wanderTime += Time.deltaTime;
        //find a random point where to walk to
        if (reachedWanderDestination)
        {
            if(wanderTime > randomWanderTime) 
            {
                float randomZ = Random.Range(-wanderingArea, wanderingArea);
                float randomX = Random.Range(-wanderingArea, wanderingArea);
                randomWanderTime = Random.Range(minWanderTime, maxWanderTime);
                wanderDestination = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
                reachedWanderDestination = false;
            }
        }

        if (!reachedWanderDestination) 
        {
            wanderTime = 0f;
            agent.destination = wanderDestination;  
        }
    }
    //curious-shy

    private void Shy()
    {
        var cubeRenderer = gameObject.GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_Color", Color.yellow);
        
        //have the enemy stay at a point at a specific distance from the player 
        //we need the player's position and a generic distance to hold
        shyTime += Time.deltaTime;
        if (shyTime > randomShyTime) 
        {
            float randomX = maxDisToPlayer;
            float randomZ = maxDisToPlayer;
            int chanceToAggro = Random.Range(1, 3);
            if (chanceToAggro % 3 == 1)
            {
                isAggroed = true;
            }
            shyTime = 0f;
            randomShyTime = Random.Range(minShyTime, maxShyTime);
            if (player.position.x < transform.position.x)
            {
                randomX = Random.Range((maxDisToPlayer/2), maxDisToPlayer);
            } else {
                randomX = Random.Range(-(maxDisToPlayer/2), -maxDisToPlayer);
            }
            if (player.position.z < transform.position.z)
            {
                randomZ = Random.Range((maxDisToPlayer/2), maxDisToPlayer);
            } else {
                randomZ = Random.Range(-(maxDisToPlayer/2), maxDisToPlayer);
            }
            //float randomX = Random.Range(0f, maxDisToPlayer);
            //float randomZ = Random.Range(0f, maxDisToPlayer);

            float specZ = player.position.z + randomZ;
            float specX = player.position.x + randomX;

            agent.destination = new Vector3(player.position.x + specX, player.position.y, player.position.z + specZ);
        }
        

    }

    //Aggressive
    //within range of the player to attack
    private void Aggressive()
    {
        var cubeRenderer = gameObject.GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_Color", Color.red);
        attackTimer += Time.deltaTime;

        changeDistance += Time.deltaTime;

        agent.destination = chasePlayer;
        //agent.destination = new Vector3(player.position.x + randomX, player.position.y, player.position.z + randomZ);

        if (changeDistance > 5f)
        {
            float randomX = Random.Range(-attackRange, attackRange);
            float randomZ = Random.Range(-attackRange, attackRange);
            chasePlayer = new Vector3(player.position.x + randomX, player.position.y, player.position.z + randomZ);
            changeDistance = 0f;
        }

        Vector3 vectorToPlayer = player.position - transform.position;
        float distanceToPlayer = vectorToPlayer.magnitude;
        if (distanceToPlayer <= attackRange && attackTimer >= attackCooldown) 
        {
            float angle = Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x) * 180 / Mathf.PI;
            int continueAttack = Random.Range(1, 3);
            Attack(vectorToPlayer.normalized, angle);
            if (continueAttack % 3 == 1)
            {
                isAggroed = false;
            }
            attackTimer = 0f;
        }
    }

    private void Attack(Vector3 towardsPlayer, float angle)
    {
        //attackPrefab.transform.position = transform.position + new Vector3(3, 0, 3);
        Instantiate(attackPrefab, transform.position + towardsPlayer, new Quaternion(angle, 0, 0, 0));
    }
    

}
