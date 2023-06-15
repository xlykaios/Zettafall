using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    private BoxCollider boxCollider;
    [SerializeField] private Transform teleportPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        print(other);
        if (other.transform.tag == "Player")
        {
            other.transform.position = teleportPoint.position;
        }
    }
}
