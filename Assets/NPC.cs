using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// #ISSUE_1 : Text appearing as repetitive, with a strange behaviour if the asset starts with a space
// #ISSUE_2 : Gian wants it to be as a text bubble instead of the UI CANVAS behaviour, probably i can use the parallax images as built by niko for the puzzle?
// Making some static bubbles that appear on character interaction

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;

    public GameObject continuebutton;
    public float wordSpeed;
    public bool playerIsClose;


    
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && playerIsClose) {
            if (dialoguePanel.activeInHierarchy) {
                zeroText();
            }
            else {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }

        }
        if(dialogueText.text == dialogue[index]) {
            continuebutton.SetActive(true);
        }
    }

    public void zeroText() {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing() {

        /*
        foreach(char Letter in dialogue[index].ToCharArray()) {
            dialogueText.text += Letter;
            yield return new WaitForSeconds(wordSpeed);
        }*/

        foreach(char Phrase in dialogue[index]) {
            dialogueText.text += Phrase;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void nextLine() {
        continuebutton.SetActive(false);
        if (index < dialogue.Length - 1) {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());

        }
        else {
            zeroText();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}
