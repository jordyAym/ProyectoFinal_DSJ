// Assets/Scripts/Game/Managers/PlanetManager.cs
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlanetManager : MonoBehaviour
{
    [Header("Datos del Planeta")]
    public PlanetData data;

    [Header("Referencias UI")]
    public UIManager uiManager;

    private IEnumerator Start()
    {
        if (data == null)
        {
            Debug.LogWarning("PlanetManager: No se asignó un PlanetData.");
            yield break;
        }

        ApplyEnvironmentSettings();
        uiManager.InitPlanet(data);

        // Popup de bienvenida
        uiManager.ShowPopup($"¡Bienvenido a {data.planetName}!");
        yield return new WaitForSeconds(4f);

        // Popup del dato relevante
        if (!string.IsNullOrWhiteSpace(data.keyFact))
            uiManager.ShowPopup(data.keyFact);
    }

    private void ApplyEnvironmentSettings()
    {
        // Skybox
        if (data.skybox != null)
            RenderSettings.skybox = data.skybox;
        else
            Debug.Log("PlanetManager: sin skybox asignado.");

        // Fog
        RenderSettings.fogColor = data.fogColor;

        // Audio ambiental
        var ambientSource = GetComponent<AudioSource>();
        if (data.ambientAudio != null)
        {
            ambientSource.clip = data.ambientAudio;
            ambientSource.loop = true;
            ambientSource.Play();
        }
        else
        {
            Debug.Log("PlanetManager: sin audio ambiental asignado.");
        }
    }
}

