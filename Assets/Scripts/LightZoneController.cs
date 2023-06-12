using System.Collections;
using UnityEngine;

public class LightZoneController : MonoBehaviour
{
    public float moonlightIntensity = 0.2f;
    public Color moonlightColor = Color.white;
    public Color moonlightAmbientColor = Color.white;
    public float transitionSpeed = 1f;

    private float originalIntensity;
    private Color originalColor;
    private Color originalAmbientColor;
    private Light directionalLight;

    private void Start()
    {
        // Assuming your Directional Light (sun) is tagged as "Sun"
        directionalLight = GameObject.FindGameObjectWithTag("Sun").GetComponent<Light>();
        originalIntensity = directionalLight.intensity;
        originalColor = directionalLight.color;
        originalAmbientColor = RenderSettings.ambientLight;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the zone is the player
        if (other.CompareTag("Player"))
        {
            // Start the transition to the moonlight
            StopAllCoroutines(); // Stop any ongoing transitions
            StartCoroutine(TransitionToLightState(moonlightIntensity, moonlightColor, moonlightAmbientColor));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object leaving the zone is the player
        if (other.CompareTag("Player"))
        {
            // Start the transition back to the sunlight
            StopAllCoroutines(); // Stop any ongoing transitions
            StartCoroutine(TransitionToLightState(originalIntensity, originalColor, originalAmbientColor));
        }
    }

    private IEnumerator TransitionToLightState(float targetIntensity, Color targetColor, Color targetAmbientColor)
    {
        while (Mathf.Abs(directionalLight.intensity - targetIntensity) > 0.01f 
            || ColorDifference(directionalLight.color, targetColor) > 0.01f
            || ColorDifference(RenderSettings.ambientLight, targetAmbientColor) > 0.01f)
        {
            directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, targetIntensity, transitionSpeed * Time.deltaTime);
            directionalLight.color = Color.Lerp(directionalLight.color, targetColor, transitionSpeed * Time.deltaTime);
            RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, targetAmbientColor, transitionSpeed * Time.deltaTime);
            yield return null;
        }
        // Ensure the target state is reached
        directionalLight.intensity = targetIntensity; 
        directionalLight.color = targetColor;
        RenderSettings.ambientLight = targetAmbientColor;
    }

    private float ColorDifference(Color a, Color b)
    {
        float diffR = Mathf.Abs(a.r - b.r);
        float diffG = Mathf.Abs(a.g - b.g);
        float diffB = Mathf.Abs(a.b - b.b);
        return diffR + diffG + diffB;
    }
}
