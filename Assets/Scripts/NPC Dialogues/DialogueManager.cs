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

    public AudioSource typingSoundEffectSource;
    public AudioClip[] typingSoundEffects;

    private Queue<string> sentences;
    public bool inDialogue { get; private set; }

    // Add this line
    public bool firstConvoIsActive = false;

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

        // If this is the first conversation, set firstConvoIsActive to true
        if (!dialogue.firstConvoOver)
        {
            firstConvoIsActive = true;
        }

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
            if (!char.IsWhiteSpace(letter)) 
            {
                AudioClip typingSoundEffect = typingSoundEffects[Random.Range(0, typingSoundEffects.Length)];
                typingSoundEffectSource.clip = typingSoundEffect;
                typingSoundEffectSource.Play();
            }
            yield return null;
        }
    }

    public void EndDialogue()
    {
        dialogueUI.SetActive(false);
        inDialogue = false;

        // Add this line. When the dialogue ends, set firstConvoIsActive to false
        firstConvoIsActive = false;

        nameText.text = "";
        dialogueText.text = "";
    }
}
