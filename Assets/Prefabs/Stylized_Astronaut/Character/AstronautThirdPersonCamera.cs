using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AstronautThirdPersonCamera
{
    public class AstronautThirdPersonCamera : MonoBehaviour
    {
        [Header("Camera Settings")]
        public Transform target; // El jugador
        public float distance = 5.0f;
        public float height = 2.0f;
        public float smoothSpeed = 10.0f;

        [Header("Mouse Settings")]
        public float mouseSensitivity = 2.0f;
        public float minYAngle = -60f;
        public float maxYAngle = 60f;

        [Header("Collision")]
        public bool enableCameraCollision = true;
        public LayerMask collisionLayers = -1;
        public float minDistance = 1.0f;

        private float mouseX = 0f;
        private float mouseY = 0f;
        private float currentDistance;
        private Camera cam;

        void Start()
        {
            cam = GetComponent<Camera>();
            currentDistance = distance;

            // Bloquear cursor al centro de la pantalla
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Inicializar rotación basada en el target
            if (target != null)
            {
                Vector3 angles = transform.eulerAngles;
                mouseX = angles.y;
                mouseY = angles.x;
            }
        }

        void LateUpdate()
        {
            if (target == null) return;

            // Manejar el cursor
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Cursor.lockState == CursorLockMode.Locked)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }

            // Solo procesar mouse input si el cursor está bloqueado
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                float mouseInputX = Input.GetAxis("Mouse X");
                float mouseInputY = Input.GetAxis("Mouse Y");

                mouseX += mouseInputX * mouseSensitivity;
                mouseY -= mouseInputY * mouseSensitivity;
                mouseY = Mathf.Clamp(mouseY, minYAngle, maxYAngle);
            }

            // Calcular posición de la cámara
            Vector3 targetPosition = target.position + Vector3.up * height;

            // Calcular rotación y dirección
            Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
            Vector3 direction = rotation * Vector3.back;

            // Verificar colisiones para ajustar distancia
            float finalDistance = distance;
            if (enableCameraCollision)
            {
                RaycastHit hit;
                if (Physics.Raycast(targetPosition, direction, out hit, distance, collisionLayers))
                {
                    finalDistance = Mathf.Clamp(hit.distance - 0.1f, minDistance, distance);
                }
            }

            Vector3 desiredPosition = targetPosition + direction * finalDistance;

            // Aplicar posición y rotación directamente (sin Lerp para evitar el bucle)
            transform.position = desiredPosition;
            transform.rotation = rotation;

            // Zoom con scroll del mouse
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0f)
            {
                distance = Mathf.Clamp(distance - scroll * 2f, 2f, 10f);
            }
        }

        // Método para cambiar el target dinámicamente
        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }

        // Método para resetear la cámara detrás del jugador
        public void ResetCameraPosition()
        {
            if (target != null)
            {
                mouseX = target.eulerAngles.y;
                mouseY = 0f;
            }
        }

        void OnDrawGizmosSelected()
        {
            // Visualizar el rango de la cámara en el editor
            if (target != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(target.position + Vector3.up * height, distance);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(target.position + Vector3.up * height, minDistance);
            }
        }
    }
}