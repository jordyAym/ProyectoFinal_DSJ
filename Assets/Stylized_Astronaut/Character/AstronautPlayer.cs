using UnityEngine;
using System.Collections;

namespace AstronautPlayer
{
    [RequireComponent(typeof(CharacterController))]
    public class AstronautPlayer : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float walkSpeed = 6.0f;
        public float runSpeed = 12.0f;
        public float jumpHeight = 2.0f;       // Altura deseada en metros
        public float rotationSpeed = 10.0f;

        [Header("Ground Check")]
        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;

        private Animator anim;
        private CharacterController controller;
        private Vector3 velocity;
        private bool isGrounded;
        private Camera playerCamera;

        void Start()
        {
            controller = GetComponent<CharacterController>();
            anim = GetComponentInChildren<Animator>();
            playerCamera = Camera.main;

            // Crear groundCheck si no existe
            if (groundCheck == null)
            {
                var go = new GameObject("GroundCheck");
                go.transform.SetParent(transform);
                go.transform.localPosition = new Vector3(0, -1f, 0);
                groundCheck = go.transform;
            }
        }

        void Update()
        {
            // 1) Ground Check
            isGrounded = Physics.CheckSphere(
                groundCheck.position,
                groundDistance,
                groundMask
            );

            if (isGrounded && velocity.y < 0f)
            {
                // Mantiene contacto con el suelo
                velocity.y = -2f;
            }

            // 2) Movimiento horizontal
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float speed = isRunning ? runSpeed : walkSpeed;

            Vector3 dir;
            if (playerCamera != null)
            {
                var f = playerCamera.transform.forward;
                var r = playerCamera.transform.right;
                f.y = 0; r.y = 0;
                f.Normalize(); r.Normalize();
                dir = f * v + r * h;
            }
            else
            {
                dir = new Vector3(h, 0, v);
            }

            if (dir.magnitude >= 0.1f)
            {
                dir.Normalize();
                controller.Move(dir * speed * Time.deltaTime);
                anim.SetInteger("AnimationPar", isRunning ? 2 : 1);

                // Rotación suave hacia la dirección de movimiento
                var targetRot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRot,
                    rotationSpeed * Time.deltaTime
                );
            }
            else
            {
                anim.SetInteger("AnimationPar", 0);
            }

            // 3) Salto (usa la fórmula v = √(2·g·h))
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                float g = -Physics.gravity.y;  // gravedad positiva
                velocity.y = Mathf.Sqrt(2f * g * jumpHeight);
                anim.SetTrigger("Jump");
            }

            // 4) Aplicar gravedad global
            velocity.y += Physics.gravity.y * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        void OnDrawGizmosSelected()
        {
            if (groundCheck != null)
            {
                Gizmos.color = isGrounded ? Color.green : Color.red;
                Gizmos.DrawSphere(groundCheck.position, groundDistance);
            }
        }
    }
}
