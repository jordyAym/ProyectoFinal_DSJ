using UnityEngine;

public class SpaceshipWaypoints : MonoBehaviour
{
    [Header("Waypoints")]
    public Transform[] waypoints;
    public float speed = 10f;
    public float rotationSpeed = 2f;
    public float waypointReachDistance = 2f;

    [Header("Opciones")]
    public bool loop = true; // Si true, vuelve al inicio
    public bool smoothRotation = true;

    private int currentWaypointIndex = 0;
    private bool movingForward = true;

    void Update()
    {
        if (waypoints.Length == 0) return;

        MoveToWaypoint();
    }

    void MoveToWaypoint()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Mover hacia el waypoint
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Rotar hacia el waypoint
        if (smoothRotation)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.LookAt(targetWaypoint);
        }

        // Verificar si llegó al waypoint
        float distance = Vector3.Distance(transform.position, targetWaypoint.position);
        if (distance < waypointReachDistance)
        {
            GoToNextWaypoint();
        }
    }

    void GoToNextWaypoint()
    {
        if (loop)
        {
            // Modo loop: vuelve al inicio
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        else
        {
            // Modo ping-pong: va y vuelve
            if (movingForward)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = waypoints.Length - 2;
                    movingForward = false;
                }
            }
            else
            {
                currentWaypointIndex--;
                if (currentWaypointIndex < 0)
                {
                    currentWaypointIndex = 1;
                    movingForward = true;
                }
            }
        }
    }

    // Visualizar waypoints en el editor
    void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Gizmos.color = Color.cyan;

        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] != null)
            {
                // Dibujar esfera en cada waypoint
                Gizmos.DrawWireSphere(waypoints[i].position, 1f);

                // Dibujar línea al siguiente waypoint
                if (i < waypoints.Length - 1 && waypoints[i + 1] != null)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                }

                // Si es loop, conectar último con primero
                if (loop && i == waypoints.Length - 1 && waypoints[0] != null)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawLine(waypoints[i].position, waypoints[0].position);
                }
            }
        }
    }
}