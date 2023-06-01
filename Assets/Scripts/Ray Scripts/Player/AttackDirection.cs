using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDirection : MonoBehaviour
{
    //private Transform cameraPos;
    private float horzLastUpdate;
    private float vertLastUpdate;
    // Start is called before the first frame update
    void Start()
    {
        //cameraPos = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vectorNorm = (transform.parent.position - Camera.main.transform.position).normalized;
        vectorNorm = new Vector3(vectorNorm.x, 0, vectorNorm.z);
        vectorNorm.Normalize();
        vectorNorm *= 40;
        float horzCurrent = Input.GetAxisRaw("Horizontal");
        float vertCurrent = Input.GetAxisRaw("Vertical");
        /*if (horzCurrent )
        {

        }*/
        if((horzCurrent != horzLastUpdate || vertCurrent != vertLastUpdate) && horzCurrent != 0 || vertCurrent != 0)
        {
             Vector3 origin = transform.position - gameObject.transform.parent.position;
            transform.localPosition = vectorNorm;
            if (horzCurrent == 1)
            {
                transform.localPosition = new Vector3(transform.localPosition.z, 0, -transform.localPosition.x);
            }
            if (horzCurrent == -1)
            {
                transform.localPosition = new Vector3(-transform.localPosition.z, 0, transform.localPosition.x);        
            }
            if (vertCurrent == -1)
            {
                transform.localPosition = new Vector3(-transform.localPosition.x, 0, -transform.localPosition.z);                    
            }
            horzLastUpdate = horzCurrent;
            vertLastUpdate = vertCurrent;
        }
        
    }
}
