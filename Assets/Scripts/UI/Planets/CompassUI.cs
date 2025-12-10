// Assets/Scripts/UI/Planets/CompassUI.cs
using UnityEngine;

public class CompassUI : MonoBehaviour
{
    [SerializeField] private RectTransform arrow;
    [SerializeField] private Transform player;
    [SerializeField] private Transform target;  // p.ej. la nave o punto de aterrizaje

    void Update()
    {
        Vector3 dir = (target.position - player.position).normalized;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        arrow.localEulerAngles = new Vector3(0, 0, -angle);
    }
}
