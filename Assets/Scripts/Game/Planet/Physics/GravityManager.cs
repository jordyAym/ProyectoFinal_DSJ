// Assets/Scripts/Game/Physics/GravityManager.cs
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    [Tooltip("PlanetData que contiene la gravedad en m/s²")]
    public PlanetData planetData;

    void Start()
    {
        // Ajusta la gravedad global de Unity al valor del planeta
        Physics.gravity = Vector3.down * planetData.gravity;
    }
}
