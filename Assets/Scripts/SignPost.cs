using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignPost : MonoBehaviour
{
    public GameObject player;
    public UIManager uiManager;
    public string message = "Default signpost message";
    public float readDistance = 3f;
    public KeyCode readKey = KeyCode.E;

    private bool isReading = false;

    private void Start()
    {
        uiManager.HideSignPostText();
        uiManager.HidePromptText();
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if(distance <= readDistance)
        {
            uiManager.RegisterSignPost(this, distance);

            if (Input.GetKeyDown(readKey))
            {
                isReading = !isReading;

                if (isReading)
                {
                    uiManager.ShowSignPostText(message); // Show the signpost text
                }
                else
                {
                    uiManager.HideSignPostText(); // Hide the signpost text
                }
            }
        }
        else
        {
            uiManager.UnregisterSignPost(this);
            if (isReading)
            {
                isReading = false;
                uiManager.HideSignPostText(); // Hide the signpost text
            }
        }
    }
}
