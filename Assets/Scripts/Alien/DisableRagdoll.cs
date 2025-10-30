using UnityEngine;

public class DisableRagdoll : MonoBehaviour
{
    void Awake()
    {
        // Desactivar todos los Rigidbody excepto el principal
        Rigidbody mainRB = GetComponent<Rigidbody>();

        Rigidbody[] allRigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in allRigidbodies)
        {
            if (rb != mainRB) // No tocar el Rigidbody principal
            {
                rb.isKinematic = true;
            }
        }

        // Destruir todos los Character Joints
        CharacterJoint[] joints = GetComponentsInChildren<CharacterJoint>();
        foreach (CharacterJoint joint in joints)
        {
            Destroy(joint);
        }

        // Opcional: Desactivar colliders de las extremidades
        Collider mainCollider = GetComponent<Collider>();
        Collider[] allColliders = GetComponentsInChildren<Collider>();
        foreach (Collider col in allColliders)
        {
            if (col != mainCollider)
            {
                col.enabled = false;
            }
        }
    }
}