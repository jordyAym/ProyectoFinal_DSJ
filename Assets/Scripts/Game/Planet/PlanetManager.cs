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

    [Header("Sistema de Misión - Robo de Nave")]
    public bool enableShipHeistMission = true;
    public int componentsNeeded = 3;
    private int componentsCollected = 0;

    [Header("Referencias Misión")]
    public GameObject controlPanel;
    public GameObject escapeZone;
    public GameObject alienShip;

    [Header("Estado Misión")]
    public bool panelUnlocked = false;
    public bool panelHacked = false;
    public bool missionComplete = false;

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

        // Iniciar misión si está habilitada
        if (enableShipHeistMission)
        {
            yield return new WaitForSeconds(2f);
            InitShipHeistMission();
        }
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

    // ==================== SISTEMA DE MISIÓN ====================

    private void InitShipHeistMission()
    {
        // Asegurarse de que el panel y zona de escape estén desactivados
        if (controlPanel) controlPanel.SetActive(false);
        if (escapeZone) escapeZone.SetActive(false);

        // Mostrar objetivo inicial
        uiManager.ShowPopup("MISIÓN: Busca componentes para robar la nave alien");
        UpdateMissionUI();
    }

    public void CollectComponent()
    {
        componentsCollected++;
        UpdateMissionUI();

        uiManager.ShowPopup($"Componente recolectado: {componentsCollected}/{componentsNeeded}");
        Debug.Log($"Componente recolectado: {componentsCollected}/{componentsNeeded}");

        if (componentsCollected >= componentsNeeded)
        {
            UnlockPanel();
        }
    }

    private void UnlockPanel()
    {
        panelUnlocked = true;
        if (controlPanel) controlPanel.SetActive(true);

        uiManager.ShowPopup("¡Todos los componentes recolectados!\nVe a la nave alien y hackea el panel");
        Debug.Log("¡Panel desbloqueado!");
    }

    public void PanelHacked()
    {
        panelHacked = true;
        if (escapeZone) escapeZone.SetActive(true);

        uiManager.ShowPopup("¡Panel hackeado! ¡SUBE A LA NAVE Y ESCAPA!");
        Debug.Log("¡Nave lista para escape!");
    }

    public void MissionComplete()
    {
        missionComplete = true;
        uiManager.ShowPopup("¡MISIÓN COMPLETADA!\n¡Has escapado de Júpiter!");
        Debug.Log("¡VICTORIA!");

        // Reiniciar después de 5 segundos
        Invoke("RestartLevel", 5f);
    }

    private void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }

    private void UpdateMissionUI()
    {
        // Aquí puedes actualizar tu UI existente
        // Por ejemplo, si tienes un método en UIManager para actualizar misiones
        // uiManager.UpdateMissionProgress(componentsCollected, componentsNeeded);
    }
}