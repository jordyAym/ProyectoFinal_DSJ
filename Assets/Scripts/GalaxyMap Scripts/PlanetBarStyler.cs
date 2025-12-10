using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(HorizontalLayoutGroup))]
public class PlanetBarStyler : MonoBehaviour
{
    [Header("Botón")]
    public Vector2 buttonSize = new Vector2(140, 50);
    public Sprite backgroundSprite;               // Asigna aquí un sprite 9-slice con esquinas redondeadas
    public Color normalColor = Color.white;
    public Color highlightedColor = new Color(0.9f, 0.9f, 1f);
    public Color pressedColor = new Color(0.7f, 0.7f, 1f);

    [Header("Texto TMP")]
    public TMP_FontAsset tmpFont;
    public int fontSize = 20;
    public Color fontColor = Color.black;

    [Header("Hover Effect")]
    public float hoverScale = 1.1f;               // Factor de escala al pasar el ratón

    void OnValidate() => ApplyStyle();
    void Start() => ApplyStyle();

    void ApplyStyle()
    {
        // Configura el layout
        var layout = GetComponent<HorizontalLayoutGroup>();
        layout.spacing = 15;
        layout.padding = new RectOffset(20, 20, 10, 10);
        layout.childAlignment = TextAnchor.MiddleCenter;
        layout.childForceExpandWidth = false;
        layout.childForceExpandHeight = false;

        // Recorre cada botón hijo
        foreach (Transform child in transform)
        {
            var btn = child.GetComponent<Button>();
            var rt = child.GetComponent<RectTransform>();
            if (btn == null || rt == null) continue;

            // 1) Tamaño
            rt.sizeDelta = buttonSize;

            // 2) Imagen de fondo
            var img = btn.GetComponent<Image>();
            if (backgroundSprite != null)
            {
                img.sprite = backgroundSprite;
                img.type = Image.Type.Sliced;
            }

            // 3) Colores
            var cb = btn.colors;
            cb.normalColor = normalColor;
            cb.highlightedColor = highlightedColor;
            cb.pressedColor = pressedColor;
            btn.colors = cb;

            // 4) Texto TMP según nombre del GameObject
            var tmp = child.GetComponentInChildren<TextMeshProUGUI>();
            if (tmp != null)
            {
                tmp.text = child.gameObject.name;
                tmp.fontSize = fontSize;
                tmp.color = fontColor;
                if (tmpFont != null) tmp.font = tmpFont;
                tmp.alignment = TextAlignmentOptions.Center;
            }

            // 5) Efecto hover: añade el componente si no existe
            var hover = child.GetComponent<ButtonScaleOnHover>();
            if (hover == null)
            {
                hover = child.gameObject.AddComponent<ButtonScaleOnHover>();
            }
            hover.hoverScale = hoverScale;
        }
    }
}
