using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseBehaviour : MonoBehaviour
{
    public GameObject drop;

    void OnDestroy()
    {
        SpawnDrop();
    }

    private void SpawnDrop()
    {
        if(drop != null)
        {
            Instantiate(drop, transform.position, Quaternion.identity);
        }
    }

}
