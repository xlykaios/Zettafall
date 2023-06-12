using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeKnockback : MonoBehaviour
{
    public float kbDurationMitigation;
    public float kbStrenghtMitigation;
    private float kbDuration;
    private float kbStrenght;
    private Vector3 kbDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(kbDuration > 0f)
        {
            kbDuration -= Time.deltaTime;
            transform.position += kbDirection * kbStrenght * Time.deltaTime;
        }
    }

    public void ReceiveKnockback(float kbStrenghtParam, float kbDurationParam, Vector3 kbDirectionParam)
    {
        kbDirection = kbDirectionParam;
        kbDuration = kbDurationParam - kbDurationMitigation;
        kbStrenght = kbStrenghtParam - kbStrenghtMitigation;
        if(kbStrenght < 0f)
        {
            kbStrenght = 0f;
        }
    }

    

}
