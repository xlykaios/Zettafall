using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject interactionPrompt;

    private bool playerInRange = false;

    private void Start()
    {
        interactionPrompt.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && !DialogueManager.instance.inDialogue)
        {
            interactionPrompt.SetActive(false);
            DialogueManager.instance.StartDialogue(dialogue);
        }

        else if (Input.GetKeyDown(KeyCode.E) && playerInRange && DialogueManager.instance.inDialogue)
        {
            DialogueManager.instance.DisplayNextSentence();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            DialogueManager.instance.EndDialogue();
            interactionPrompt.SetActive(false);
        }
    }
}
