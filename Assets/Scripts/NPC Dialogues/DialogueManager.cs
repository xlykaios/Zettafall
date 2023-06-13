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

    public AudioSource typingSoundEffectSource; // Assign your AudioSource component in the inspector
    public AudioClip[] typingSoundEffects; // Assign an array of your sound effects in the inspector

    private Queue<string> sentences;
    private Dialogue currentDialogue;

    public bool inDialogue { get; private set; }

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

    public Dialogue CurrentDialogue()
    {
        return currentDialogue;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        inDialogue = true;
        dialogueUI.SetActive(true);

        currentDialogue = dialogue;

        nameText.text = dialogue.name;

        sentences.Clear();

        if (!dialogue.firstConvoOver)
        {
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            dialogue.firstConvoOver = true;
        }
        else
        {
            int randomIndex = Random.Range(0, dialogue.postFirstConvoSentences.Length);
            sentences.Enqueue(dialogue.postFirstConvoSentences[randomIndex]);
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
            if (!char.IsWhiteSpace(letter))  // Only play the sound on non-whitespace characters
            {
                // Pick a random sound effect from the array
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
        nameText.text = "";
        dialogueText.text = "";
        currentDialogue = null;
    }
}

