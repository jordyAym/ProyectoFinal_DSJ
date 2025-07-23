// Assets/Scripts/UI/Planets/ScientificLogUI.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScientificLogUI : MonoBehaviour
{
    [SerializeField] private Transform contentPanel;
    [SerializeField] private GameObject entryPrefab;

    public void AddEntry(Sprite icon, string description)
    {
        GameObject entry = Instantiate(entryPrefab, contentPanel);
        entry.transform.Find("Icon").GetComponent<Image>().sprite =
            icon;
        entry.transform.Find("Text").GetComponent<TextMeshProUGUI>().text =
            description;
    }
}
