using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI signPostText;
    public TextMeshProUGUI promptText;
    public GameObject textBoxPanel;

    private Dictionary<SignPost, float> signpostsInRange = new Dictionary<SignPost, float>();
    private bool isReading = false;

    void Start()
    {
        textBoxPanel.SetActive(false);
    }

    public void RegisterSignPost(SignPost signPost, float distance)
    {
        signpostsInRange[signPost] = distance;
        UpdatePromptText();
    }

    public void UnregisterSignPost(SignPost signPost)
    {
        if (signpostsInRange.ContainsKey(signPost))
        {
            signpostsInRange.Remove(signPost);
            UpdatePromptText();
        }
    }

    private void UpdatePromptText()
    {
        if (signpostsInRange.Count > 0 && !isReading)
        {
            ShowPromptText("Press E to read");
        }
        else
        {
            HidePromptText();
        }
    }

    public void ShowSignPostText(string message)
    {
        signPostText.text = message;
        textBoxPanel.SetActive(true);
        isReading = true;
    }

    public void HideSignPostText()
    {
        signPostText.text = "";
        textBoxPanel.SetActive(false);
        isReading = false;
    }

    public void ShowPromptText(string message)
    {
        promptText.text = message;
    }

    public void HidePromptText()
    {
        promptText.text = "";
    }
}
