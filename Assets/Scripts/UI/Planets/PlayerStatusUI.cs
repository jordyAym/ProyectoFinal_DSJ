// Assets/Scripts/UI/Planets/PlayerStatusUI.cs
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour
{
    [SerializeField] private Slider oxygenSlider, tempSlider, radSlider;
    [SerializeField] private TextMeshProUGUI stateText;

    public void UpdateStats(float oxygen, float suitTemp, float radiation, string state)
    {
        oxygenSlider.value = oxygen;
        tempSlider.value = suitTemp;
        radSlider.value = radiation;
        stateText.text = state;
    }
}
