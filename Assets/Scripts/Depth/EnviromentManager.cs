using UnityEngine;

public class Enviroment : MonoBehaviour
{
    [Header("Fog Settings")]
    [SerializeField] private bool enableFog = true;
    [SerializeField] private Color fogColor = Color.gray;
    [SerializeField] private float fogDensity = 0.05f; // For Exponential modes
    [SerializeField] private float startDistance = 0f; // For Linear mode
    [SerializeField] private float endDistance = 300f; // For Linear mode
    
    [Header("Fog Mode")]
    [SerializeField] private FogMode fogMode = FogMode.ExponentialSquared;

    private void Start()
    {
        ApplyFogSettings();
    }

    // Call this to update fog at runtime
    public void ApplyFogSettings()
    {
        RenderSettings.fog = enableFog;
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogMode = fogMode;
        
        // Apply specific settings based on mode
        if (fogMode == FogMode.Linear)
        {
            RenderSettings.fogStartDistance = startDistance;
            RenderSettings.fogEndDistance = endDistance;
        }
        else
        {
            RenderSettings.fogDensity = fogDensity;
        }
    }

    // Helper method to change color smoothly via script
    public void SetFogColor(Color newColor)
    {
        RenderSettings.fogColor = newColor;
    }

    // Helper to change density
    public void SetFogDensity(float density)
    {
        RenderSettings.fogDensity = density;
    }
}
