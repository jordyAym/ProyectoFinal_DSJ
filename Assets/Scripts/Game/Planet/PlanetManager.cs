using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controla la configuración ambiental de un planeta
public class PlanetManager : MonoBehaviour
{
    // ScriptableObject con los datos del planeta
    public PlanetData data;

    void Start()
    {
        if (data != null)
        {
            ApplySettings();
        }
        else
        {
            Debug.LogWarning("PlanetManager: No se asignó un PlanetData.");
        }
    }

    void ApplySettings()
    {
        // ⚠️ Gravedad: asegúrate que no sea cero
        if (data.gravity != 0)
        {
            Physics.gravity = Vector3.down * data.gravity;
        }
        else
        {
            Debug.LogWarning("PlanetManager: gravedad no válida, usando 9.81 por defecto.");
            Physics.gravity = Vector3.down * 9.81f;
        }

        // 🌌 Skybox
        if (data.skybox != null)
        {
            RenderSettings.skybox = data.skybox;
        }
        else
        {
            Debug.Log("PlanetManager: sin skybox asignado.");
        }

        // 🌫️ Fog color
        RenderSettings.fogColor = data.fogColor;  // puede dejarse así porque el Color nunca es null

        // 🔊 Sonido ambiental
        GameObject ambientObj = GameObject.Find("AmbientAudio");
        if (ambientObj != null)
        {
            AudioSource ambient = ambientObj.GetComponent<AudioSource>();
            if (ambient != null)
            {
                if (data.ambientAudio != null)
                {
                    ambient.clip = data.ambientAudio;
                    ambient.Play();
                }
                else
                {
                    Debug.Log("PlanetManager: sin audio ambiental asignado.");
                }
            }
            else
            {
                Debug.LogWarning("PlanetManager: AmbientAudio no tiene AudioSource.");
            }
        }
        else
        {
            Debug.LogWarning("PlanetManager: no se encontró objeto 'AmbientAudio' en la escena.");
        }
    }
}
