using UnityEngine;

public class FadingMobile : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeSpeed = 0.5f;
    public float inactiveAlpha = 0.5f;
    public float activeAlpha = 1.0f;
    private bool isFading = false;

    private void Update()
    {
        if (isFading)
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, activeAlpha, fadeSpeed * Time.deltaTime);
        }
        else
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, inactiveAlpha, fadeSpeed * Time.deltaTime);
        }
    }

    public void Activate()
    {
        isFading = true;
    }

    public void Deactivate()
    {
        isFading = false;
    }
}
