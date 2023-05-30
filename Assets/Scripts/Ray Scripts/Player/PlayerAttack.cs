using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public LayerMask enemyLayers;
    public KeyCode attackKey = KeyCode.Space;
    public GameObject swordPrefab;
    public GameObject specialPrefab;
    private Animator anim;
    private float attackTimer;
    private int comboCounter;
    public float minComboTime = 0.5f;
    public float maxComboTime = 1f;
    public int maxCombo = 3;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        if (Input.GetKeyDown(attackKey))
        {
            Attack();
        }
    }

    private void Attack()
    {
        if(attackTimer > maxComboTime && comboCounter > 0)
        {
            comboCounter = 0;
        }
        if(attackTimer > maxComboTime)
        {
            attackTimer = 0f;
            comboCounter ++;
            return;
        }
        if(attackTimer > minComboTime && comboCounter == maxCombo)
        {
            UseSpecialAttack();
            comboCounter = 0;
            attackTimer = 0f;
            return;
        }
        if(attackTimer > minComboTime && comboCounter > 0)
        {
            UseAttack();
            comboCounter++;
            attackTimer = 0f;
            return;
        }
    }
    private void UseAttack()
    {
        Instantiate(swordPrefab, GameObject.Find("AttackForward").GetComponent<Transform>().position, Quaternion.identity);      
    }

    private void UseSpecialAttack()
    {
        Instantiate(specialPrefab, GameObject.Find("AttackForward").GetComponent<Transform>().position, Quaternion.identity);      
    }

    /*
    public void AttackHitEvent()
    {
        Collider[] enemiesToDamage = Physics.OverlapSphere(transform.position, attackRange, enemyLayers);
        DealDamage(enemiesToDamage);
    }

    private void DealDamage(Collider[] enemiesToDamage)
    {
        int damage = 1;
        foreach (Collider enemy in enemiesToDamage)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }*/
}