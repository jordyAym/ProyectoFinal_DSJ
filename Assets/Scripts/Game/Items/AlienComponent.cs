// Assets/Scripts/Game/Items/AlienComponent.cs
using UnityEngine;

public class AlienComponent : MonoBehaviour
{
    [Header("Configuración")]
    public string componentName = "Componente Alien";
    public Color glowColor = Color.cyan;

    [Header("Efectos")]
    public bool rotateConstantly = true;
    public float rotationSpeed = 50f;

    private Renderer rend;
    private Light glowLight;

    void Start()
    {
        SetupVisuals();
    }

    void Update()
    {
        if (rotateConstantly)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    void SetupVisuals()
    {
        // Brillo del material
        rend = GetComponent<Renderer>();
        if (rend && rend.material)
        {
            rend.material.color = glowColor;

            // Emission (si el shader lo soporta)
            if (rend.material.HasProperty("_EmissionColor"))
            {
                rend.material.EnableKeyword("_EMISSION");
                rend.material.SetColor("_EmissionColor", glowColor * 2f);
            }
        }

        // Luz punto
        glowLight = gameObject.AddComponent<Light>();
        glowLight.type = LightType.Point;
        glowLight.color = glowColor;
        glowLight.range = 5f;
        glowLight.intensity = 2f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectComponent();
        }
    }

    void CollectComponent()
    {
        // Buscar PlanetManager y notificar
        PlanetManager manager = FindObjectOfType<PlanetManager>();
        if (manager)
        {
            manager.CollectComponent();
        }

        // Efecto visual simple (opcional)
        if (glowLight)
        {
            glowLight.intensity = 10f;
        }

        Debug.Log($"Recolectado: {componentName}");

        // Destruir después de medio segundo
        Destroy(gameObject, 0.5f);
    }
}