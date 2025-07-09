using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFocus : MonoBehaviour
{
    public GameObject target;

    private void OnMouseDown()
    {
        Focus.target = target;
        Camera.main.fieldOfView = Mathf.Clamp(1 * target.transform.localScale.x, 1, 100);
    }
}
