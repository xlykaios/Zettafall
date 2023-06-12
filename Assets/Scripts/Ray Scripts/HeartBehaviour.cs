using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBehaviour : MonoBehaviour
{
    public int healing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag.Equals("Player") && collider.GetComponent<Health>() != null)
        {
            Health health = collider.gameObject.GetComponent<Health>();
            health.HealHP(healing);
            Destroy(gameObject);
        }
    }

}
