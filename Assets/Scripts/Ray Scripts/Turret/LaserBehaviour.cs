using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    private LineRenderer lr;
    public Transform player;
    public Transform turret;
    private int lenght = 2;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = lenght;
        player = GameObject.Find("PlayerObj").transform;
        turret = GameObject.Find("Turret").transform;
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, turret.position);
        lr.SetPosition(1, player.position);
    }
}
