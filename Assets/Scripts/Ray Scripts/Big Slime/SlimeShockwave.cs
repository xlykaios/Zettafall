using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeShockwave : MonoBehaviour
{
    public float sizeIncrease;
    public float attackDuration;
    public int damage;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.localScale += new Vector3(sizeIncrease, 0, sizeIncrease) * Time.deltaTime;
        if(timer >= attackDuration)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.GetComponent<Health>() != null && collider.gameObject.tag.Equals("Player") && (transform.position - collider.transform.position).magnitude >= (transform.localScale.x/2 - 1f))
        {
            
            Health health = collider.GetComponent<Health>();
            health.TakeDamage(damage);
            
        }
    }

}
