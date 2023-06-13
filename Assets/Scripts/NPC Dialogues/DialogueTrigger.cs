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
        if (distanceToPlayer <= interactionRange && Input.GetKeyDown(KeyCode.E))
        {
            // Check if a dialogue is already in progress
            if (!DialogueManager.instance.inDialogue)
            {
                // If not, start dialogue and hide the interaction prompt
                interactionPrompt.SetActive(false);
                DialogueManager.instance.StartDialogue(dialogue);
            }
            else if (DialogueManager.instance.CurrentDialogue() == dialogue)
            {
                // If a dialogue is in progress and it is this dialogue, display the next sentence
                DialogueManager.instance.DisplayNextSentence();
            }
        }
        else if (distanceToPlayer > interactionRange)
        {
            interactionPrompt.SetActive(false);
            if (DialogueManager.instance.CurrentDialogue() == dialogue)
            {
                // If the player is out of range and this dialogue is active, end the dialogue
                DialogueManager.instance.EndDialogue();
            }
        }
        else
        {
            interactionPrompt.SetActive(true);
        }
    }
}
