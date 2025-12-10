using UnityEngine;
using TMPro;
using UnityEngine.Rendering;  // para AmbientMode

[RequireComponent(typeof(Light))]
public class DayNightCycle : MonoBehaviour
{
    [Header("Datos del Planeta")]
    public PlanetData planetData;

    [Header("Control de Tiempo")]
    [Tooltip("1 = tiempo real; >1 acelera el ciclo")]
    public float timeMultiplier = 1000f;

    [Header("Mínimos nocturnos")]
    [Range(0f, 1f)] public float minSunIntensity = 0.2f;
    [Range(0f, 1f)] public float minAmbientIntensity = 0.35f;

    [Header("UI Reloj")]
    [Tooltip("Arrastra aquí tu componente TextMeshProUGUI del reloj")]
    public TextMeshProUGUI clockText;

    private Light sunLight;
    private float cycleSeconds, elapsed;
    private AnimationCurve intensityCurve;
    private Gradient lightColorGradient, ambientColorGradient;

    void Awake()
    {
        sunLight = GetComponent<Light>();
        RenderSettings.ambientMode = AmbientMode.Flat;

        // Preconfigura curvas y degradados
        CreateDefaultCurvesAndGradients();
    }

    void Start()
    {
        // Duración total del ciclo en segundos
        cycleSeconds = planetData.dayLength * 3600f;
    }

    void Update()
    {
        // 1) Avanzar el tiempo usando el multiplicador
        elapsed = (elapsed + Time.deltaTime * timeMultiplier) % cycleSeconds;
        float t = elapsed / cycleSeconds;

        // 2) Rotar el sol
        float angle = Mathf.Lerp(-90f, 270f, t);
        sunLight.transform.rotation = Quaternion.Euler(angle, 170f, 0f);

        // 3) Intensidad y color del sol
        float rawInt = intensityCurve.Evaluate(t);
        sunLight.intensity = Mathf.Max(rawInt, minSunIntensity);
        sunLight.color = lightColorGradient.Evaluate(t);

        // 4) Luz ambiente
        Color amb = ambientColorGradient.Evaluate(t);
        amb.r = Mathf.Max(amb.r, minAmbientIntensity);
        amb.g = Mathf.Max(amb.g, minAmbientIntensity);
        amb.b = Mathf.Max(amb.b, minAmbientIntensity);
        RenderSettings.ambientLight = amb;

        // 5) Exposición de skybox (si existe)
        if (RenderSettings.skybox.HasProperty("_Exposure"))
        {
            float exp = Mathf.Lerp(minAmbientIntensity, 1f, rawInt);
            RenderSettings.skybox.SetFloat("_Exposure", exp);
        }

        // 6) Actualizar el reloj en pantalla
        if (clockText != null)
        {
            float totalHours = t * planetData.dayLength;
            int hours = Mathf.FloorToInt(totalHours);
            int minutes = Mathf.FloorToInt((totalHours - hours) * 60f);
            clockText.text = $"Horas: {hours:00} - Minutos: {minutes:00}";
        }
    }

    private void CreateDefaultCurvesAndGradients()
    {
        intensityCurve = new AnimationCurve(
            new Keyframe(0f, 0f),
            new Keyframe(0.25f, 0.8f),
            new Keyframe(0.5f, 1f),
            new Keyframe(0.75f, 0.8f),
            new Keyframe(1f, 0f)
        );
        for (int i = 0; i < intensityCurve.length; i++)
            intensityCurve.SmoothTangents(i, 1f);

        lightColorGradient = new Gradient();
        lightColorGradient.SetKeys(
            new[] {
                new GradientColorKey(Color.black,   0f),
                new GradientColorKey(new Color(1,0.5f,0), .25f),
                new GradientColorKey(Color.white,   .5f),
                new GradientColorKey(new Color(1,0.5f,0), .75f),
                new GradientColorKey(Color.black,   1f)
            },
            new[] {
                new GradientAlphaKey(1f,0f), new GradientAlphaKey(1f,1f)
            }
        );

        ambientColorGradient = new Gradient();
        ambientColorGradient.SetKeys(
            new[] {
                new GradientColorKey(Color.black,               0f),
                new GradientColorKey(new Color(0.3f,0.2f,0.1f), .25f),
                new GradientColorKey(new Color(0.5f,0.5f,0.5f), .5f),
                new GradientColorKey(new Color(0.3f,0.2f,0.1f), .75f),
                new GradientColorKey(Color.black,               1f)
            },
            new[] {
                new GradientAlphaKey(1f,0f), new GradientAlphaKey(1f,1f)
            }
        );
    }
}
