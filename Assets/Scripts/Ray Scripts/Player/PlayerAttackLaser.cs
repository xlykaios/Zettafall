using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackLaser : MonoBehaviour
{
    public float attackCooldown;
    private float attackTimer;
    public KeyCode attackKey;
    public GameObject laser;
    private LineRenderer laserLine;
    private int laserLenght = 2;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        laserLine = laser.GetComponent<LineRenderer>();
        laserLine.positionCount = laserLenght;
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        if (Input.GetKeyDown(attackKey))
        {
            Attack();
        }    
    }

    GameObject FindClosestTarget()
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float currDis = (enemy.transform.position - transform.position).sqrMagnitude;
            if(currDis < distance)
            {
                closest = enemy;
                distance = currDis;
            }
        }
        return closest;
    }

    void Attack()
    {
        if(attackCooldown > attackTimer)
        {
            return;
        }
        attackTimer = 0f;
        GameObject target = FindClosestTarget();
        Health targetHealth = target.GetComponent<Health>();
        targetHealth.TakeDamage(damage);
        laserLine.enabled = true;
        laserLine.SetPosition(0, transform.position);        
        laserLine.SetPosition(1, target.transform.position);
        StartCoroutine(FadeLaser());
    }

    IEnumerator FadeLaser()
    {
        for(float timer = 0f; timer < 0.3f; timer += Time.deltaTime)
        {
            yield return null;
        }
        laserLine.enabled = false;
    }

}
