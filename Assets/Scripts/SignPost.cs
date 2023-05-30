using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignPost : MonoBehaviour
{
    public GameObject player; 
    public UIManager uiManager; // Reference to the UIManager
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

        if(distance <= readDistance && !isReading) 
        {
            uiManager.ShowPromptText("Press E to read"); // Show the prompt
        }
        else
        {
            uiManager.HidePromptText(); // Hide the prompt
        }

        if(Input.GetKeyDown(readKey) && distance <= readDistance)
        {
            isReading = !isReading;

            if(isReading)
            {
                uiManager.HidePromptText(); // Hide the prompt
                uiManager.ShowSignPostText(message); // Show the signpost text
            }
            else
            {
                uiManager.ShowPromptText("Press E to read"); // Show the prompt
                uiManager.HideSignPostText(); // Hide the signpost text
            }
        }

        if(distance > readDistance && isReading)
        {
            isReading = false;
            uiManager.HideSignPostText(); // Hide the signpost text
        }
    }
}
