using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueUI;

    private Queue<string> sentences;

    public bool inDialogue;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Fix It! More than one DialogueManager instance found!");
            return;
        }

        instance = this;

        sentences = new Queue<string>();
        dialogueUI.SetActive(false);
        inDialogue = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        inDialogue = true;
        dialogueUI.SetActive(true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);  // You can adjust the typing speed by changing the waiting time.
        }
    }

    public void EndDialogue()
    {
        dialogueUI.SetActive(false);
        inDialogue = false;
        // Add these lines
        nameText.text = "";
        dialogueText.text = "";
    }

    public void HideText()
    {
        nameText.text = "";
        dialogueText.text = "";
    }
}
