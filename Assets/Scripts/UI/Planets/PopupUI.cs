// Assets/Scripts/UI/Planets/PopupUI.cs
using System.Collections;
using TMPro;
using UnityEngine;

public class PopupUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float displayDuration = 3f;

    public void Show(string message)
    {
        StopAllCoroutines();
        messageText.text = message;
        canvasGroup.alpha = 0;
        gameObject.SetActive(true);
        StartCoroutine(FadeInOut());
    }

    private IEnumerator FadeInOut()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            canvasGroup.alpha = t / fadeDuration;
            t += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
        yield return new WaitForSeconds(displayDuration);
        t = fadeDuration;
        while (t > 0f)
        {
            canvasGroup.alpha = t / fadeDuration;
            t -= Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}
