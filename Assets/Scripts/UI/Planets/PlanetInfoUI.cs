// Assets/Scripts/UI/Planets/PlanetInfoUI.cs
using TMPro;
using UnityEngine;

public class PlanetInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText, gravityText, tempText, dayLengthText, atmText, radText;

    public void SetData(PlanetData data)
    {
        nameText.text = data.planetName;
        gravityText.text = $"{data.gravity:0.0} m/s²";
        tempText.text = $"{data.averageTemperature:0.0} °C";
        dayLengthText.text = $"{data.dayLength:0.0} h";
        atmText.text = data.atmosphereStatus;
        radText.text = $"{data.radiationLevel:0.0} µSv/h";
    }
}