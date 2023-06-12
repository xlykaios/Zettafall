using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Animation_Script : MonoBehaviour
{
    public Animator animator;
    private float horizontal;
    private float vertical;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       horizontal = Input.GetAxisRaw("Horizontal");
       vertical = Input.GetAxisRaw("Vertical");

       animator.SetFloat("Horizontal", horizontal);
       animator.SetFloat("Vertical", vertical);
    }
}
