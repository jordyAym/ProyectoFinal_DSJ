// Assets/Scripts/Game/Managers/UIManager.cs
using UnityEngine;
using UnityEngine.UI;  // ← Añade esta línea
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI de Planeta")]
    public PlanetInfoUI planetInfoUI;
    public PlayerStatusUI playerStatusUI;
    public CompassUI compassUI;
    public ScientificLogUI scientificLogUI;
    public PopupUI popupUI;
    public ClockUI clockUI;

    /// 
   // === Si usas Text normal ===
    [Header("UI Texts (Legacy)")]
    public Text componentTextLegacy;
    public Text missionTextLegacy;

    // === Si usas TextMeshPro ===
    [Header("UI Texts (TextMeshPro)")]
    public TMP_Text componentText;
    public TMP_Text missionText;

    [Header("Otros UI Elements")]
    public GameObject popupPanel;
    public TMP_Text popupText; //
    public void InitPlanet(PlanetData data)
    {
        planetInfoUI.SetData(data);
    }

    public void UpdatePlayer(float oxygen, float temp, float rad, string state)
    {
        playerStatusUI.UpdateStats(oxygen, temp, rad, state);
    }

    public void AddLogEntry(Sprite icon, string desc)
    {
        scientificLogUI.AddEntry(icon, desc);
    }

    public void ShowPopup(string msg)
    {
        popupUI.Show(msg);
    }

    public void UpdateMissionProgress(int current, int total)
    {
        // Si usas TextMeshPro:
        if (componentText)
        {
            componentText.text = $"Componentes: {current}/{total}";
        }

        // Si usas Text legacy:
        if (componentTextLegacy)
        {
            componentTextLegacy.text = $"Componentes: {current}/{total}";
        }
    }

    public void UpdateMissionObjective(string objective)
    {
        // Si usas TextMeshPro:
        if (missionText)
        {
            missionText.text = objective;
        }

        // Si usas Text legacy:
        if (missionTextLegacy)
        {
            missionTextLegacy.text = objective;
        }
    }
}
