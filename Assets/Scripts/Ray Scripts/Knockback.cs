using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{

    public float kbStrenght;
    public float kbDuration;
    // Start is called before the first frame update

    public void ApplyKnockback(GameObject collider)
    {
        if (collider.GetComponent<TakeKnockback>() != null)
        {
                Vector3 knockbackDirection = (collider.transform.position - transform.position).normalized;
                TakeKnockback colliderKnockback = collider.gameObject.GetComponent<TakeKnockback>();
                colliderKnockback.ReceiveKnockback(kbStrenght, kbDuration, knockbackDirection);
        }    
    }

}
