// Assets/Scripts/Data/Planets/PlanetData.cs
using UnityEngine;

[CreateAssetMenu(menuName = "Planet/PlanetData")]
public class PlanetData : ScriptableObject
{
    [Header("Identificación")]
    public string planetName;

    [Header("Propiedades Físicas")]
    public float gravity;
    public float averageTemperature;
    public float dayLength;
    public string atmosphereStatus;
    public float radiationLevel;

    [Header("Apariencia del Cielo")]
    public Material skybox;
    public Color fogColor;

    [Header("Audio Ambiental")]
    public AudioClip ambientAudio;

    [Header("Info de Bienvenida")]
    [TextArea]
    [Tooltip("Un dato curioso o relevante que quieras mostrar al cargar la escena.")]
    public string keyFact;

    private void OnValidate()
    {
        gravity = Mathf.Max(0, gravity);
        dayLength = Mathf.Max(0, dayLength);
        averageTemperature = Mathf.Clamp(averageTemperature, -273.15f, 5000f);
        radiationLevel = Mathf.Max(0, radiationLevel);
    }
}
