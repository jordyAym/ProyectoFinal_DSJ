using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class OptionsController : MonoBehaviour
{
    public RectTransform planetas;
    float posFinal;
    bool muestra = true;
    public float tiempo = 0.5f;

    void Start()
    {
        posFinal = Screen.width / 2;
        planetas.position = new Vector3 (-posFinal, planetas.position.y, 0);
    }

    IEnumerator Mover(float time, Vector3 posI, Vector3 posF)
    {
        float elapsedTime = 0.1f;
        while (elapsedTime < time)
        {
            planetas.position = Vector3.Lerp(posI, posF, (elapsedTime/time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        planetas.position = posF;
    }

    void MoverOptsPlanetas(float time, Vector3 posI, Vector3 posF)
    {
        StartCoroutine(Mover(time, posI, posF));
    }

    public void BtnPlanetas()
    {
        int signo = 1;
        if (!muestra)
            signo = -1;

        //MoverOptsPlanetas (tiempo, planetas)
    }
}
