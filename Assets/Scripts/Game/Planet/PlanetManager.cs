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

    // Unity detecta esto como coroutine si devuelve IEnumerator
    private IEnumerator Start()
    {
        if (data == null)
        {
            Debug.LogWarning("PlanetManager: No se asignó un PlanetData.");
            yield break;
        }

        ApplyEnvironmentSettings();
        uiManager.InitPlanet(data);

        // 1) Popup de bienvenida
        uiManager.ShowPopup($"¡Bienvenido a {data.planetName}!");

        // 2) Esperamos el ciclo completo de fade-in + display + fade-out (por defecto 0.5 + 3 + 0.5 = 4s)
        yield return new WaitForSeconds(4f);

        // 3) Popup del dato relevante (si existe)
        if (!string.IsNullOrWhiteSpace(data.keyFact))
            uiManager.ShowPopup(data.keyFact);
    }

    private void ApplyEnvironmentSettings()
    {
        // Gravedad
        if (data.gravity > 0)
            Physics.gravity = Vector3.down * data.gravity;
        else
        {
            Debug.LogWarning("PlanetManager: gravedad no válida, usando 9.81 por defecto.");
            Physics.gravity = Vector3.down * 9.81f;
        }

        // Skybox
        if (data.skybox != null)
            RenderSettings.skybox = data.skybox;
        else
            Debug.Log("PlanetManager: sin skybox asignado.");

        // Fog color
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

    // Llamar cuando recolectes una muestra
    public void OnCollectSample(Sprite icon, string description)
    {
        uiManager.AddLogEntry(icon, description);
        uiManager.ShowPopup(description);
    }

    // Llamar al cambiar stats del jugador
    public void OnPlayerStatsChanged(float oxygen, float suitTemp, float radiation, string state)
    {
        uiManager.UpdatePlayer(oxygen, suitTemp, radiation, state);
    }
}
