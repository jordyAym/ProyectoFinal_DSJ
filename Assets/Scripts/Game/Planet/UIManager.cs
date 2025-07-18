// Assets/Scripts/Game/Managers/UIManager.cs
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI de Planeta")]
    public PlanetInfoUI planetInfoUI;
    public PlayerStatusUI playerStatusUI;
    public CompassUI compassUI;
    public ScientificLogUI scientificLogUI;
    public PopupUI popupUI;
    public ClockUI clockUI;

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
}
