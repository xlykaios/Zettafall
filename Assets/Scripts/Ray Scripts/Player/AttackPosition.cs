using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPosition : MonoBehaviour
{
    private Transform playerPos;
    public float offset = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("PlayerObj").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
       transform.position = playerPos.position;
       // Vector3 tempRotation = new Vector3(cameraPos.rotation.x, 0, cameraPos.rotation.z);
       // transform.eulerAngles = tempRotation;
        /*transform.position = playerPos.position;
        Vector3 tempRotation = new Vector3(cameraPos.rotation.x, 0, cameraPos.rotation.z);
        transform.rotation = */
        
    }
}
