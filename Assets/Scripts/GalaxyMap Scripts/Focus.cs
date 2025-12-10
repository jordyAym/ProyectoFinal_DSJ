using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Focus : MonoBehaviour
{
    public static GameObject target;
           
    void Update()
    {
        if (target)
            transform.LookAt(target.transform);
    }
}
