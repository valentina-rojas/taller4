using UnityEngine;
using UnityEngine.UI;

public class TendCat : MonoBehaviour
{
    [Header("Cepillado")]
    public RectTransform cepilloUI;
    public RectTransform areaCepilladoUI;  // Zona más amplia para detectar el cepillado
    public float tiempoNecesario = 2f;

    [Header("Alimentar")]
    public RectTransform bolsaComidaUI;
    public RectTransform platitoUI;
    public Sprite platitoLlenoSprite; 

    private float tiempoSobreAreaCepillado = 0f;
    private bool tareaCepillarCompletada = false;
    private bool tareaAlimentarCompletada = false;

    private Camera camara;

    private void Start()
    {
        camara = Camera.main;
    }

    private void Update()
    {
        VerificarCepillado();
        VerificarAlimentacion();
    }

    private void VerificarCepillado()
    {
        if (tareaCepillarCompletada) return;

        // Obtenemos la posición del cepillo en pantalla
        Vector2 posicionCepillo = RectTransformUtility.WorldToScreenPoint(camara, cepilloUI.position);

        // Chequeamos si el punto del cepillo está dentro del área de cepillado
        if (RectTransformUtility.RectangleContainsScreenPoint(areaCepilladoUI, posicionCepillo, camara))
        {
            tiempoSobreAreaCepillado += Time.deltaTime;
            Debug.Log($"Cepillando al gato: {tiempoSobreAreaCepillado:F2}s");

            if (tiempoSobreAreaCepillado >= tiempoNecesario)
            {
                tareaCepillarCompletada = true;
                Debug.Log("Gato cepillado correctamente");
                TaskManager.instance.CompletarTareaPorID(2);
            }
        }
        else
        {
            tiempoSobreAreaCepillado = 0f;
        }
    }

    private void VerificarAlimentacion()
    {
        if (tareaAlimentarCompletada) return;

        Rect rectPlatito = GetScreenRect(platitoUI);
        Rect rectBolsa = GetScreenRect(bolsaComidaUI);

        if (rectPlatito.Overlaps(rectBolsa))
        {
            tareaAlimentarCompletada = true;
            Debug.Log("Gato alimentado correctamente (detectado por Overlaps)");
            TaskManager.instance.CompletarTareaPorID(3);

            Image platitoImage = platitoUI.GetComponent<Image>();
            if (platitoImage != null && platitoLlenoSprite != null)
            {
                platitoImage.sprite = platitoLlenoSprite;
            }
        }
    }

    private Rect GetScreenRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);

        Vector2 bottomLeft = RectTransformUtility.WorldToScreenPoint(camara, corners[0]);
        Vector2 topRight = RectTransformUtility.WorldToScreenPoint(camara, corners[2]);

        return new Rect(bottomLeft, topRight - bottomLeft);
    }
}
