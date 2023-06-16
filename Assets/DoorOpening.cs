using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour
{

    public GameObject door1;
    public GameObject door2;
    public CapsuleCollider collider;
    private bool doorState;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (doorState)
        {
            door1.transform.Translate(Vector3.right * 0.3f);
            door2.transform.Translate(Vector3.right * -0.3f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            doorState = true;
            Destroy(gameObject, 1);
        }
    }
}
