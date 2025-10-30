// Assets/Scripts/Game/Player/AstronautPlayer.cs
using UnityEngine;

namespace AstronautPlayer
{
    [RequireComponent(typeof(CharacterController))]
    public class AstronautPlayer : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float walkSpeed = 6f;
        public float runSpeed = 12f;
        public float jumpHeight = 2f;   // Altura deseada en metros
        public float rotationSpeed = 10f;

        [Header("Ground Check")]
        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;

        private Animator anim;
        private CharacterController controller;
        private Vector3 velocity;
        private bool isGrounded;
        private Camera playerCamera;

        void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        void Start()
        {
            anim = GetComponentInChildren<Animator>();
            playerCamera = Camera.main;

            if (groundCheck == null)
            {
                var go = new GameObject("GroundCheck");
                go.transform.SetParent(transform);
                go.transform.localPosition = new Vector3(0, -1f, 0);
                groundCheck = go.transform;
            }

            // Debug opcional: comprobar que la gravedad global ya está bien
            Debug.Log($"AstronautPlayer: Physics.gravity.y = {Physics.gravity.y} m/s²");
        }

        void Update()
        {
            // 1) Ground check
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0f)
                velocity.y = -2f;

            // 2) Movimiento horizontal
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float speed = isRunning ? runSpeed : walkSpeed;

            Vector3 dir;
            if (playerCamera != null)
            {
                var f = playerCamera.transform.forward; f.y = 0; f.Normalize();
                var r = playerCamera.transform.right; r.y = 0; r.Normalize();
                dir = f * v + r * h;
            }
            else
            {
                dir = new Vector3(h, 0, v);
            }

            if (dir.magnitude >= 0.1f)
            {
                controller.Move(dir.normalized * speed * Time.deltaTime);
                anim.SetInteger("AnimationPar", isRunning ? 2 : 1);

                // Rotación suave
                var targetRot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            }
            else
            {
                anim.SetInteger("AnimationPar", 0);
            }

            // 3) Salto
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                float g = -Physics.gravity.y; // gravedad positiva
                velocity.y = Mathf.Sqrt(2f * g * jumpHeight);
                anim.SetTrigger("Jump");
            }

            // 4) Aplicar gravedad
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
