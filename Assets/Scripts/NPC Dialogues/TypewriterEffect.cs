using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public AudioSource typingSoundEffectSource; // Assign your AudioSource component in the inspector
    public AudioClip[] typingSoundEffects; // Assign an array of your sound effects in the inspector

    private TextMeshProUGUI tmpText;

    private void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
    }

    public void DisplayText(string text)
    {
        StopAllCoroutines(); // Stops any ongoing TypeSentence coroutines
        StartCoroutine(TypeSentence(text));
    }

    IEnumerator TypeSentence(string sentence)
    {
        tmpText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            tmpText.text += letter;
            if (!char.IsWhiteSpace(letter))  // Only play the sound on non-whitespace characters
            {
                // Pick a random sound effect from the array
                AudioClip typingSoundEffect = typingSoundEffects[Random.Range(0, typingSoundEffects.Length)];
                StartCoroutine(PlaySoundAfterDelay(typingSoundEffect, 0.00f));  // Play sound after a delay of 0.05 seconds
            }
            yield return new WaitForSeconds(0.1f); // Add a delay between each letter. You can adjust the delay time as needed.
        }
    }

    IEnumerator PlaySoundAfterDelay(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        typingSoundEffectSource.clip = clip;
        typingSoundEffectSource.Play();
    }
}
