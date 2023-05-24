using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BikeBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 player;
    public Transform[] points;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.Find("PlayerObj").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((points[index].position - transform.position).magnitude > 1f)
        {
            agent.destination = points[index].position;
        } else
        {
            index += 1;
            if (index >= points.Length)
            {
                index = 0;
            }
        }
    }
}
