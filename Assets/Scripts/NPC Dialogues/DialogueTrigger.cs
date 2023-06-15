using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject interactionPrompt;
    public float interactionRange = 5f;  // Distance within which the player can interact with the NPC. You can adjust this as needed.
    private GameObject player;  // Reference to the player game object
    private bool inRange;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");  // Assumes the player game object is tagged with "Player"
        interactionPrompt.SetActive(false);
    }

    private void Update()
    {
        // Check the distance between the player and the NPC
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= interactionRange)
        {
            if (!inRange)
            {
                inRange = true;
                interactionPrompt.SetActive(true);
            }

            if (!DialogueManager.instance.inDialogue && Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.instance.StartDialogue(dialogue);
                interactionPrompt.SetActive(false);
            }
            else if (DialogueManager.instance.CurrentDialogue() == dialogue && Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.instance.DisplayNextSentence();
            }
        }
        else
        {
            if (inRange)
            {
                inRange = false;
                interactionPrompt.SetActive(false);
            }

            if (DialogueManager.instance.CurrentDialogue() == dialogue)
            {
                DialogueManager.instance.EndDialogue();
            }
        }
    }
}
