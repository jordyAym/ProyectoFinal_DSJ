// Assets/Scripts/Game/Interactions/ShipControlPanel.cs
using UnityEngine;
using UnityEngine.UI;

public class ShipControlPanel : MonoBehaviour
{
    [Header("Configuración Hackeo")]
    public float hackTime = 10f;
    public KeyCode interactKey = KeyCode.E;
    public float interactionRange = 3f;

    private float hackProgress = 0f;
    private bool isHacking = false;
    private bool isHacked = false;

    [Header("UI Referencias")]
    public Slider hackSlider;
    public Text hackPromptText;
    public GameObject hackUIPanel; // Tu HackUI de abajo izquierda

    [Header("Efectos")]
    public Color hackingColor = Color.yellow;
    public Color completeColor = Color.green;

    private Renderer rend;
    private GameObject player;

    void Start()
    {
        rend = GetComponent<Renderer>();

        if (hackUIPanel) hackUIPanel.SetActive(false);
        if (hackSlider)
        {
            hackSlider.value = 0f;
            hackSlider.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isHacked) return;

        // Buscar jugador
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (player)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance < interactionRange)
            {
                ShowPrompt(true);

                if (Input.GetKey(interactKey))
                {
                    PerformHack();
                }
                else if (Input.GetKeyUp(interactKey))
                {
                    StopHacking();
                }
            }
            else
            {
                ShowPrompt(false);
                StopHacking();
            }
        }
    }

    void ShowPrompt(bool show)
    {
        if (hackPromptText)
        {
            hackPromptText.text = show ? "Mantén [E] para hackear" : "";
        }
    }

    void PerformHack()
    {
        if (!isHacking)
        {
            isHacking = true;
            if (hackUIPanel) hackUIPanel.SetActive(true);
            if (hackSlider) hackSlider.gameObject.SetActive(true);

            // Cambiar color del panel
            if (rend) rend.material.color = hackingColor;

            Debug.Log("Iniciando hackeo...");
        }

        // Progreso
        hackProgress += Time.deltaTime;

        if (hackSlider)
        {
            hackSlider.value = hackProgress / hackTime;
        }

        if (hackProgress >= hackTime)
        {
            CompleteHack();
        }
    }

    void StopHacking()
    {
        if (isHacking && !isHacked)
        {
            isHacking = false;
            hackProgress = 0f;

            if (hackSlider)
            {
                hackSlider.value = 0f;
                hackSlider.gameObject.SetActive(false);
            }

            if (hackUIPanel) hackUIPanel.SetActive(false);

            // Restaurar color
            if (rend) rend.material.color = Color.white;
        }
    }

    void CompleteHack()
    {
        isHacked = true;
        isHacking = false;

        if (hackSlider) hackSlider.gameObject.SetActive(false);
        if (hackUIPanel) hackUIPanel.SetActive(false);
        if (hackPromptText) hackPromptText.text = "";

        // Color de completado
        if (rend) rend.material.color = completeColor;

        // Notificar al PlanetManager
        PlanetManager manager = FindObjectOfType<PlanetManager>();
        if (manager)
        {
            manager.PanelHacked();
        }

        Debug.Log("¡Panel hackeado exitosamente!");
    }

    // Visualizar rango en editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}