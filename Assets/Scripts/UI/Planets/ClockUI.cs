// Assets/Scripts/UI/Planets/ClockUI.cs
using UnityEngine;
using TMPro;

public class ClockUI : MonoBehaviour
{
    [SerializeField] private PlanetData planetData;
    [SerializeField] private TextMeshProUGUI clockText;

    private float cycleSeconds;
    private float elapsed;

    void Start()
    {
        cycleSeconds = planetData.dayLength * 3600f;
    }

    void Update()
    {
        elapsed = (elapsed + Time.deltaTime) % cycleSeconds;
        float normalized = elapsed / cycleSeconds;
        float totalHours = normalized * planetData.dayLength;
        int hours = Mathf.FloorToInt(totalHours);
        int minutes = Mathf.FloorToInt((totalHours - hours) * 60f);
        clockText.text = $"{hours:00}:{minutes:00}";
    }
}
