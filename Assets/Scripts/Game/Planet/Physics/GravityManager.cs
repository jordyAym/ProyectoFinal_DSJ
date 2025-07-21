// Assets/Scripts/Game/Physics/GravityManager.cs
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    [Tooltip("PlanetData que contiene la gravedad en m/s²")]
    public PlanetData planetData;

    void Start()
    {
        if (planetData == null)
        {
            Debug.LogWarning("GravityManager: no PlanetData asignado");
            return;
        }
        // Ajusta la gravedad global de Unity
        Physics.gravity = Vector3.down * planetData.gravity;
    }
}
