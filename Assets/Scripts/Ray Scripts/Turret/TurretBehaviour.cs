using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    private Transform player;
    //aggro
    public float aggroRange;
    //aiming
    //rotate towards player
    //use laser pointer
    private LineRenderer laser;
    //shooting
    //stop rotating
    //remove laser point
    public float attackCooldown;
    private float attackTimer;
    public float reloadCooldown;
    private float reloadTimer;
    public GameObject turretShot;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerObj").transform;
        laser = GameObject.Find("LaserPointer").GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if((player.position - transform.position).magnitude < aggroRange)
        {
            CheckState();
        } 
    }

    void OnDestroy()
    {
        GameObject.Find("LaserPointer").GetComponent<LaserBehaviour>().enabled = false;
    }
    void CheckState()
    {
        if(attackCooldown > attackTimer)
        {
            Aiming();
        }
        if(attackCooldown <= attackTimer)
        {
            Shooting();
        }
    }
    void Aiming()
    {
        attackTimer += Time.deltaTime;
        transform.LookAt(player);
        transform.Rotate(new Vector3(0, 90, 90));
        if(attackCooldown <= attackTimer)
        {
            laser.enabled = false;
            reloadTimer = 0f;
            StartCoroutine(Fire());
        }
    }

    void Shooting()
    {
        reloadTimer += Time.deltaTime;
        if(reloadTimer > reloadCooldown)
        {
            laser.enabled = true;
            attackTimer = 0f;
        }
    }

    IEnumerator Fire()
    {
        for(int i = 0; i <= 3; i++)
        {
            Instantiate(turretShot, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);  
        }
    }
}
