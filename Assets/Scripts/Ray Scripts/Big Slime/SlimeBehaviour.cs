using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : MonoBehaviour
{
    public Transform player;
    public Transform landZone;
    public GameObject shockwave;
    private float timer = 5f;
    public float jumpDuration;
    private bool isJump;
    private Vector3 destination;
    private float jumpHeight;
    private float speed;
    private bool isFall;
    private Collider hitbox;
    //aggro
    public float aggroRange;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerObj").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
    }
    void CheckState()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 && !isJump)
        {
            isJump = true;
            timer = jumpDuration;
            DirectionOfJump();
        }
        if(isJump)
        {
            Jump();
            if(timer <= 0)
            {
                isJump = false;
                isFall = true;
                timer = 5f;
            }
        }
        if(isFall)
        {
            Fall();
        }
    }
    //y = K/x
    //y = jump height
    //k = constant
    //x = distance to player / 2
    void DirectionOfJump()
    {
        jumpHeight = 1000/(player.position - transform.position).magnitude;
        destination = new Vector3(player.position.x, player.position.y + jumpHeight, player.position.z);
        speed = (destination - transform.position).magnitude/jumpDuration;
    }

    void Jump()
    {
        transform.position += (destination - transform.position).normalized * speed * Time.deltaTime; 
    }
    
    void Fall()
    {
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        if((transform.position.y - landZone.position.y) < 1f)
        {
            isFall = false;
            Instantiate(shockwave, transform.position - new Vector3(0, transform.localScale.y/2, 0), Quaternion.identity);
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag.Equals("Player"))
        {
            misonosmerdato blockPlayer = player.GetComponent<misonosmerdato>();
            blockPlayer.enabled = false;
        }
        
    }
    void OnTriggerExit(Collider collider)
    {
        if(collider.tag.Equals("Player"))
        {
            misonosmerdato blockPlayer = player.GetComponent<misonosmerdato>();
            blockPlayer.enabled = true; 
        }
          
    }
}
