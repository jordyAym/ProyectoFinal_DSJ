using UnityEngine;

public class AlienBodyAnimation : MonoBehaviour
{
    [Header("Referencias")]
    public Transform body; // Arrastra aquí el objeto "Body"
    public Transform head; // Arrastra aquí el objeto "Head"

    [Header("Configuración de Bounce (Rebote)")]
    public float bounceSpeed = 8f;
    public float bounceAmount = 0.15f;

    [Header("Configuración de Balanceo")]
    public float swaySpeed = 6f;
    public float swayAmount = 5f;

    [Header("Configuración de Cabeza")]
    public float headBobSpeed = 4f;
    public float headBobAmount = 0.1f;

    private Vector3 bodyOriginalPosition;
    private Vector3 headOriginalPosition;
    private Quaternion bodyOriginalRotation;

    private AlienWandering wanderingScript;
    private float walkTimer = 0f;

    void Start()
    {
        wanderingScript = GetComponent<AlienWandering>();

        if (body != null)
        {
            bodyOriginalPosition = body.localPosition;
            bodyOriginalRotation = body.localRotation;
        }

        if (head != null)
        {
            headOriginalPosition = head.localPosition;
        }
    }

    void Update()
    {
        // Solo animar si se está moviendo (no está esperando)
        bool isMoving = wanderingScript != null && !wanderingScript.GetIsWaiting();

        if (isMoving)
        {
            walkTimer += Time.deltaTime;
            AnimateWalking();
        }
        else
        {
            // Volver suavemente a la posición original
            ReturnToOriginalPosition();
        }
    }

    void AnimateWalking()
    {
        if (body != null)
        {
            // Rebote vertical (bounce)
            float bounce = Mathf.Sin(walkTimer * bounceSpeed) * bounceAmount;
            Vector3 newBodyPos = bodyOriginalPosition + new Vector3(0, bounce, 0);
            body.localPosition = newBodyPos;

            // Balanceo lateral (sway)
            float sway = Mathf.Sin(walkTimer * swaySpeed) * swayAmount;
            Quaternion swayRotation = Quaternion.Euler(0, 0, sway);
            body.localRotation = bodyOriginalRotation * swayRotation;
        }

        if (head != null)
        {
            // Movimiento de cabeza
            float headBob = Mathf.Sin(walkTimer * headBobSpeed) * headBobAmount;
            Vector3 newHeadPos = headOriginalPosition + new Vector3(0, headBob, 0);
            head.localPosition = newHeadPos;
        }
    }

    void ReturnToOriginalPosition()
    {
        if (body != null)
        {
            body.localPosition = Vector3.Lerp(body.localPosition, bodyOriginalPosition, Time.deltaTime * 5f);
            body.localRotation = Quaternion.Lerp(body.localRotation, bodyOriginalRotation, Time.deltaTime * 5f);
        }

        if (head != null)
        {
            head.localPosition = Vector3.Lerp(head.localPosition, headOriginalPosition, Time.deltaTime * 5f);
        }
    }
}