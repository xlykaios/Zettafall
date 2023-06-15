using UnityEngine;

public class MobileInteractionButton : MonoBehaviour
{
    public static MobileInteractionButton instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one MobileInteractionButton instance found!");
            return;
        }

        instance = this;
        gameObject.SetActive(false);
    }

    public void OnInteractionButtonPressed()
    {
        DialogueManager.instance.Interact();
    }
}
