using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruiserAttack : MonoBehaviour
{
    public int damage;
    public float attackRange;
    private float attackDuration;
    private float timeElapsed = 0f;
    public float speed;
    public Transform player;
    private bool dealKnockback;
    private Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {
        var cubeRenderer = gameObject.GetComponentInChildren<Renderer>();
        cubeRenderer.material.SetColor("_Color", Color.red);
        player = GameObject.Find("PlayerObj").GetComponent<Transform>();
        transform.LookAt(player);
        transform.Rotate(transform.rotation.x + 180, transform.rotation.y - 90, transform.rotation.z);
        //using the formula time = distance/speed, we can destroy the game object after it has travelled the desired distance
        attackDuration = attackRange / speed;
        destination = (player.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += destination * speed * Time.deltaTime;
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= attackDuration)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.GetComponent<Health>() != null && !collider.gameObject.tag.Equals("Enemy") )
        {
            Health health = collider.GetComponent<Health>();
            health.TakeDamage(damage);
            if (gameObject.GetComponent<Knockback>() != null)
            {
                Knockback knockback = gameObject.GetComponent<Knockback>();
                knockback.ApplyKnockback(collider.gameObject);
            }
        }
    }
}
