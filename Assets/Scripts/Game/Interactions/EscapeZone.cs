// Assets/Scripts/Game/Interactions/EscapeZone.cs
using UnityEngine;

public class EscapeZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerEscape();
        }
    }

    void TriggerEscape()
    {
        PlanetManager manager = FindObjectOfType<PlanetManager>();
        if (manager)
        {
            manager.MissionComplete();
        }

        Debug.Log("¡Jugador escapó en la nave!");
    }
}