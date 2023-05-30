using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignPost : MonoBehaviour
{
    public GameObject player; // Reference to the player
    public TextMeshProUGUI signPostText; // Reference to the TextMeshProUGUI to show the signpost's message
    public TextMeshProUGUI promptText; // Reference to the TextMeshProUGUI to show the prompt to read
    public string message = "Default signpost message"; // The message that will be displayed on the signpost
    public float readDistance = 3f; // How close the player needs to be to read the signpost
    public KeyCode readKey = KeyCode.E; // The key the player presses to read the signpost

    private void Start()
    {
        if(player == null)
        {
            Debug.LogError("Player GameObject is not assigned in the SignPost script.");
        }

        if(signPostText == null)
        {
            Debug.LogError("SignPostText is not assigned in the SignPost script.");
        }

        if(promptText == null)
        {
            Debug.LogError("PromptText is not assigned in the SignPost script.");
        }
    }

    private void Update()
    {
        // Check the distance between the player and the signpost
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // Log the distance to check if this part is working correctly
        Debug.Log("Distance to signpost: " + distance);

        // If the player is close enough
        if(distance <= readDistance)
        {
            // Show the prompt to read
            promptText.text = "Press E to read";
            
            // If the player presses the read key
            if(Input.GetKeyDown(readKey))
            {
                // Set the text of the signPostText to the message
                signPostText.text = message;
            }
        }
        else
        {
            // If the player is too far away, clear the signpost text and the prompt text
            signPostText.text = "";
            promptText.text = "";
        }

        // If the player releases the read key, clear the signpost text
        if(Input.GetKeyUp(readKey))
        {
            signPostText.text = "";
        }
    }
}
