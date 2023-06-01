using System.Collections;
using UnityEngine;
using TMPro;

public class ZoneTrigger : MonoBehaviour
{
    public string zoneName; // Name of the zone. Set this in the Inspector.
    public TMP_Text mainText; // Reference to the centered text. Set this in the Inspector.
    public TMP_Text visitedText; // Reference to the visited area text. Set this in the Inspector.
    private bool hasVisited; // Keep track of whether the player has visited the zone before.
    public float fadeTime = 1f; // Time it takes for the text to fade in and out.
    public AudioSource audioSource; // AudioSource component. Set this in the Inspector.
    public AudioClip zoneClip; // AudioClip to play. Set this in the Inspector.

    void Start()
    {
        // Make sure both texts are invisible at the start
        mainText.color = new Color(mainText.color.r, mainText.color.g, mainText.color.b, 0);
        visitedText.color = new Color(visitedText.color.r, visitedText.color.g, visitedText.color.b, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player. This assumes the player's tag is "Player".
        if (other.gameObject.tag == "Player")
        {
            if (!hasVisited)
            {
                // The player hasn't visited the zone before.
                StartCoroutine(DisplayMainText());
                audioSource.PlayOneShot(zoneClip); // Play the zone audio.
                hasVisited = true;
            }
            else
            {
                // The player has visited the zone before.
                visitedText.text = zoneName;
                StartCoroutine(FadeTextInAndOut(visitedText, fadeTime, fadeTime));
            }
        }
    }

    IEnumerator DisplayMainText()
    {
        mainText.text = zoneName;
        yield return StartCoroutine(FadeTextInAndOut(mainText, fadeTime, fadeTime));
        mainText.text = "";
    }

    IEnumerator FadeTextInAndOut(TMP_Text text, float inTime, float outTime)
    {
        // Fade text in
        yield return FadeTextToFullAlpha(inTime, text);

        // Wait a bit
        yield return new WaitForSeconds(5);

        // Fade text out
        yield return FadeTextToZeroAlpha(outTime, text);
    }

    IEnumerator FadeTextToFullAlpha(float t, TMP_Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    IEnumerator FadeTextToZeroAlpha(float t, TMP_Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}


