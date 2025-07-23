// Assets/Scripts/Game/Physics/GravityManager.cs
using UnityEngine;

[DisallowMultipleComponent]
public class GravityManager : MonoBehaviour
{
    [Tooltip("PlanetData que contiene la gravedad en m/s²")]
    public PlanetData planetData;

    void Awake()
    {
        if (planetData == null)
        {
            Debug.LogWarning("GravityManager: no PlanetData asignado, usando 9.81 m/s² por defecto.");
            Physics.gravity = Vector3.down * 9.81f;
        }
        else
        {
            Physics.gravity = Vector3.down * planetData.gravity;
            Debug.Log($"GravityManager: gravedad establecida a {planetData.gravity} m/s².");
        }
    }
}
