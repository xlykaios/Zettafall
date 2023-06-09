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

    public void StartDialogue(Dialogue dialogue)
{
    inDialogue = true;
    dialogueUI.SetActive(true);

    nameText.text = dialogue.name;

    sentences.Clear();

    // Choose the right sentences based on whether or not the first conversation is over
    string[] sentencesToUse = dialogue.firstConvoOver ? dialogue.postFirstConvoSentences : dialogue.sentences;
    foreach (string sentence in sentencesToUse)
    {
        sentences.Enqueue(sentence);
    }

    // If this is the first conversation, mark it as over
    if (!dialogue.firstConvoOver)
    {
        dialogue.firstConvoOver = true;
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

    IEnumerator TypeSentence (string sentence)
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
    }
}
