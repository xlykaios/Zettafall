using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignPost : MonoBehaviour
{
    public GameObject player; 
    public TextMeshProUGUI signPostText;
    public TextMeshProUGUI promptText;
    public GameObject textBoxPanel; // Reference to your Panel
    public string message = "Default signpost message";
    public float readDistance = 3f;
    public KeyCode readKey = KeyCode.E;

    private bool isReading = false;

    private void Start()
    {
        signPostText.text = "";
        promptText.text = "";
        textBoxPanel.SetActive(false); // Set the Panel inactive
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if(distance <= readDistance)
        {
            if(Input.GetKeyDown(readKey))
            {
                isReading = !isReading; 

                // Set the Panel active or inactive depending on whether the player is reading
                textBoxPanel.SetActive(isReading);
            }

            if(isReading)
            {
                signPostText.text = message;
                promptText.text = ""; // Hide the prompt while reading
            }
            else 
            {
                signPostText.text = "";
                promptText.text = "Press E to read"; // Show the prompt when not reading
            }
        }
        else
        {
            signPostText.text = "";
            promptText.text = "";
            isReading = false; 
            textBoxPanel.SetActive(false);
        }
    }
}
