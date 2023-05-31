using UnityEngine;


[RequireComponent(typeof(Light))]
public class DayNightCycle : MonoBehaviour
{
    public Gradient colorGradient;
    public Gradient ambientColorGradient;
    public float dayDuration = 120f;
    public float time = 0;

    private Light directionalLight;

    void Start()
    {
        directionalLight = GetComponent<Light>();
    }

    void Update()
    {
        time += Time.deltaTime / dayDuration;
        if (time >= 1)
        {
            time = 0;
        }

        // Update sun position and color
        transform.rotation = Quaternion.Euler(new Vector3((time * 360f) - 90f, 170, 0));
        directionalLight.color = colorGradient.Evaluate(time);

        // Update ambient lighting
        RenderSettings.ambientLight = ambientColorGradient.Evaluate(time);

        // Update skybox
        float atmosphereThickness = Mathf.Lerp(0.2f, 1f, Mathf.PingPong(time, 0.5f) * 2);
        RenderSettings.skybox.SetFloat("_AtmosphereThickness", atmosphereThickness);
        DynamicGI.UpdateEnvironment(); // This is needed to update the lighting in real-time.
    }
}
