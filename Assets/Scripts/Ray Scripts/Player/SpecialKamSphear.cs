using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialKamSphear : MonoBehaviour
{
    public float attackRange;
    public int damage;
    public float attackDuration;
    private Vector3 vectorNorm;
    private PlayerAttackSphear player;
    private float speed;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerObj").GetComponent<PlayerAttackSphear>();
        vectorNorm = (GameObject.Find("AttackForward").transform.position - transform.position).normalized;
        speed = attackRange/attackDuration;
        Vector3 direction = new Vector3(vectorNorm.z, vectorNorm.y, -vectorNorm.x);
        transform.position -= direction;
        transform.LookAt(GameObject.Find("AttackForward").transform);
        player.ToggleSphear();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.GetComponent<Health>() != null && !collider.gameObject.tag.Equals("Player"))
        {
            Health health = collider.GetComponent<Health>();
            health.TakeDamage(damage);
        }
        if (gameObject.GetComponent<Knockback>() != null && !collider.gameObject.tag.Equals("Player"))
        {
            Knockback knockback = gameObject.GetComponent<Knockback>();
            knockback.ApplyKnockback(collider.gameObject);     
        }
            
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer < attackDuration)
        {
            transform.localPosition += (vectorNorm * speed) * Time.deltaTime;
        }
        if (timer >= attackDuration)
        {
            MoveToPlayer();
        }
    }

    void MoveToPlayer()
    {
        Vector3 destination = (GameObject.Find("PlayerObj").transform.position - transform.position);
        vectorNorm = destination.normalized;
        transform.position += vectorNorm * speed * Time.deltaTime;
        if (destination.magnitude <= 1f)
        {
            player.ToggleSphear();
            Destroy(gameObject);
        }
    }
}
