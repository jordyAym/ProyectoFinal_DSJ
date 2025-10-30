using UnityEngine;

public class AlienWandering : MonoBehaviour
{
    [Header("Configuraci�n de Movimiento")]
    public float moveSpeed = 2f;
    public float rotationSpeed = 120f;

    [Header("Configuraci�n de Wandering")]
    public float wanderRadius = 10f;
    public float minWanderDistance = 3f;
    public float minWaitTime = 1f;
    public float maxWaitTime = 3f;

    [Header("Detecci�n de Obst�culos")]
    public float obstacleDetectionDistance = 2f;
    public LayerMask obstacleLayer;

    private Vector3 targetPosition;
    private float waitCounter = 0f;
    private bool isWaiting = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetNewTargetPosition();
    }

    void Update()
    {
        // Si est� esperando, no se mueve
        if (isWaiting)
        {
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                isWaiting = false;
                GetNewTargetPosition();
            }
            return;
        }

        // Detectar obst�culos adelante
        if (DetectObstacle())
        {
            // Si hay obst�culo, buscar nueva direcci�n
            GetNewTargetPosition();
            return;
        }

        // Calcular direcci�n hacia el objetivo
        Vector3 direction = (targetPosition - transform.position);
        direction.y = 0;
        float distance = direction.magnitude;
        direction.Normalize();

        // Rotar hacia el objetivo
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Moverse hacia el objetivo
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Verificar si lleg� al objetivo
        if (distance < 0.5f)
        {
            isWaiting = true;
            waitCounter = Random.Range(minWaitTime, maxWaitTime);
        }
    }

    bool DetectObstacle()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * 0.5f;

        // Raycast hacia adelante
        if (Physics.Raycast(rayOrigin, transform.forward, out hit, obstacleDetectionDistance, obstacleLayer))
        {
            Debug.DrawRay(rayOrigin, transform.forward * obstacleDetectionDistance, Color.red);
            return true;
        }

        Debug.DrawRay(rayOrigin, transform.forward * obstacleDetectionDistance, Color.green);
        return false;
    }

    void GetNewTargetPosition()
    {
        int attempts = 0;
        bool validPosition = false;

        while (!validPosition && attempts < 10)
        {
            // Generar posici�n aleatoria
            Vector2 randomCircle = Random.insideUnitCircle * wanderRadius;
            Vector3 randomPosition = new Vector3(randomCircle.x, 0, randomCircle.y);
            Vector3 potentialTarget = transform.position + randomPosition;
            potentialTarget.y = transform.position.y;

            // Verificar distancia m�nima
            float distance = Vector3.Distance(transform.position, potentialTarget);
            if (distance < minWanderDistance)
            {
                attempts++;
                continue;
            }

            // Verificar que no haya obst�culos en el camino
            Vector3 direction = (potentialTarget - transform.position).normalized;
            RaycastHit hit;

            if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, direction, out hit, distance, obstacleLayer))
            {
                targetPosition = potentialTarget;
                validPosition = true;
            }

            attempts++;
        }

        // Si no encuentra posici�n v�lida, buscar en direcci�n opuesta
        if (!validPosition)
        {
            targetPosition = transform.position - transform.forward * minWanderDistance;
            targetPosition.y = transform.position.y;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);

        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(targetPosition, 0.5f);
            Gizmos.DrawLine(transform.position, targetPosition);
        }
    }

    public bool GetIsWaiting()
    {
        return isWaiting;
    }
}