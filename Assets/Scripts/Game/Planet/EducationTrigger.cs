// Assets/Scripts/Game/Planet/EducationTrigger.cs
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EducationTrigger : MonoBehaviour
{
    [TextArea]
    [Tooltip("Mensaje educativo que se muestra al entrar")]
    public string message;
    private bool hasTriggered = false;

    void Reset()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            FindObjectOfType<UIManager>()?.ShowPopup(message);
        }
    }
}
