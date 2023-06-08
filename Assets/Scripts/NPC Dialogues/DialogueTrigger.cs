using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private bool playerInRange = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && !DialogueManager.instance.inDialogue)
        {
            DialogueManager.instance.StartDialogue(dialogue);
        }

        else if (Input.GetKeyDown(KeyCode.E) && playerInRange && DialogueManager.instance.inDialogue)
        {
            DialogueManager.instance.DisplayNextSentence();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            DialogueManager.instance.EndDialogue();
        }
    }
}

