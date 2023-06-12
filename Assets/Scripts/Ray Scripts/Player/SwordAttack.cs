using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public int damage;
    public float attackDuration;
    public float attackRange;
    private float timer;
    private float speed;
    private Vector3 vectorNorm;
    private misonosmerdato playerMovement;
    void Start()
    {
        //gesù cristo non ce la faccio più
        //tutta questa matematica
        //io sono piano cartesiano
        vectorNorm = (GameObject.Find("PlayerObj").transform.position - transform.position).normalized;
        //makes the attack start from the left of the direction the player is facing so that it feels more like a sword swing
        vectorNorm = new Vector3(vectorNorm.z, vectorNorm.y, -vectorNorm.x);
        transform.position -= vectorNorm * (attackRange/2);
        speed = attackRange/attackDuration;
        transform.LookAt(GameObject.Find("AttackForward").transform);
        playerMovement = GameObject.Find("PlayerObj").GetComponent<misonosmerdato>();
        playerMovement.enabled = false;
        //alla fine la soluzione non ci voleva nemmeno tutta quella matematica aiuto 
        //erano due fottutissime linee di codice
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

    void Update()
    {
        timer += Time.deltaTime;
        transform.localPosition += (vectorNorm * speed) * Time.deltaTime;
        if (timer >= attackDuration)
        {
            playerMovement.enabled = true;
            Destroy(gameObject);
        }
    }

}
