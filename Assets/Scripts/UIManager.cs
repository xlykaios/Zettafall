using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI signPostText;
    public TextMeshProUGUI promptText;
    public GameObject textBoxPanel;

    void Start()
    {
        textBoxPanel.SetActive(false); // Hide the panel at the start
    }

    public void ShowSignPostText(string message)
    {
        signPostText.text = message;
        textBoxPanel.SetActive(true);
    }

    public void HideSignPostText()
    {
        signPostText.text = "";
        textBoxPanel.SetActive(false);
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
