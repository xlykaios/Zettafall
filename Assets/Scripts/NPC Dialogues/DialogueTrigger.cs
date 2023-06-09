using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject interactionPrompt;

    public float interactionRange = 5f;  // Distance within which the player can interact with the NPC. You can adjust this as needed.

    private GameObject player;  // Reference to the player game object

    private void Start()
    {
        interactionPrompt.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");  // Assumes the player game object is tagged with "Player"
    }

    private void Update()
    {
        // Check the distance between the player and the NPC
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        // If the player is within range and presses E, start the dialogue
        if (distanceToPlayer <= interactionRange)
        {
            if (Input.GetKeyDown(KeyCode.E) && !DialogueManager.instance.inDialogue)
            {
                interactionPrompt.SetActive(false);
                DialogueManager.instance.StartDialogue(dialogue);
            }
            else if (Input.GetKeyDown(KeyCode.E) && DialogueManager.instance.inDialogue)
            {
                DialogueManager.instance.DisplayNextSentence();
            }

            // Check if the dialogue is not ongoing to show the interaction prompt
            if (!DialogueManager.instance.inDialogue)
            {
                interactionPrompt.SetActive(true);
            }
        }
        else
        {
            interactionPrompt.SetActive(false);
            if (DialogueManager.instance.inDialogue)
            {
                DialogueManager.instance.EndDialogue();
            }
        }
    }
}

