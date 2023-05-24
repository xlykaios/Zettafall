using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BullBehaviour : MonoBehaviour
{
    private Vector3 vectorNorm;
    public Transform player;
    public NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PlayerObj").transform;
    }

    void Update()
    {
        
    }

}
