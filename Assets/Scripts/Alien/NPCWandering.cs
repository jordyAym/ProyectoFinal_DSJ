using UnityEngine;
using UnityEngine.AI;

public class NPCWandering : MonoBehaviour
{
    [Header("Wandering Settings")]
    [SerializeField] private float wanderRadius = 10f;      // Radio de vagabundeo
    [SerializeField] private float minWaitTime = 2f;        // Tiempo mínimo de espera
    [SerializeField] private float maxWaitTime = 5f;        // Tiempo máximo de espera

    private NavMeshAgent agent;
    private float waitTimer;
    private bool isWaiting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Establecer el primer destino
        SetNewRandomDestination();
    }

    void Update()
    {
        // Si está esperando, contar el tiempo
        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0)
            {
                isWaiting = false;
                SetNewRandomDestination();
            }
        }
        // Si llegó al destino, empezar a esperar
        else if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                StartWaiting();
            }
        }
    }

    void SetNewRandomDestination()
    {
        // Generar punto aleatorio en el radio especificado
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        // Buscar el punto más cercano en el NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    void StartWaiting()
    {
        isWaiting = true;
        waitTimer = Random.Range(minWaitTime, maxWaitTime);
    }

    // Visualizar el radio de wandering en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }
}