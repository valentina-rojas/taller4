using UnityEngine;
using UnityEngine.UI;

public class TendCat : MonoBehaviour
{
    public static TendCat instance;

    [Header("Cepillado")]
    public RectTransform cepilloUI;
    public RectTransform areaCepilladoUI;
    public float tiempoNecesario = 2f;
    public Slider barraCepilladoUI;


    [Header("Alimentar")]
    public RectTransform bolsaComidaUI;
    public RectTransform platitoUI;
    public Sprite platitoLlenoSprite;
    public Sprite platitoVacioSprite;

    [Header("Sonidos")]
    public AudioSource audioSource;
    public AudioClip sonidoCepillado;
    public AudioClip sonidoComida;

    private float tiempoSobreAreaCepillado = 0f;
    private bool tareaCepillarCompletada = false;
    private bool tareaAlimentarCompletada = false;

    private Camera camara;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject); 
    }
    private void Start()
    {
        camara = Camera.main;
        if (barraCepilladoUI != null)
            barraCepilladoUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        VerificarCepillado();
        VerificarAlimentacion();
    }

    private void VerificarCepillado()
    {
        if (tareaCepillarCompletada) return;

        Vector2 posicionCepillo = RectTransformUtility.WorldToScreenPoint(camara, cepilloUI.position);

        if (RectTransformUtility.RectangleContainsScreenPoint(areaCepilladoUI, posicionCepillo, camara))
        {
            if (barraCepilladoUI != null && !barraCepilladoUI.gameObject.activeSelf)
                barraCepilladoUI.gameObject.SetActive(true);

            tiempoSobreAreaCepillado += Time.deltaTime;

            if (barraCepilladoUI != null)
                barraCepilladoUI.value = tiempoSobreAreaCepillado / tiempoNecesario;

            Debug.Log($"Cepillando al gato: {tiempoSobreAreaCepillado:F2}s");

            if (tiempoSobreAreaCepillado >= tiempoNecesario)
            {
                tareaCepillarCompletada = true;
                Debug.Log("Gato cepillado correctamente");

                if (barraCepilladoUI != null)
                {
                    barraCepilladoUI.value = 1f;
                    barraCepilladoUI.gameObject.SetActive(false);
                }

                if (audioSource != null && sonidoCepillado != null)
                    audioSource.PlayOneShot(sonidoCepillado);


                TaskManager.instance.CompletarTareaPorID(2);
            }
        }
        else
        {
            tiempoSobreAreaCepillado = 0f;

            if (barraCepilladoUI != null)
            {
                barraCepilladoUI.value = 0f;
                barraCepilladoUI.gameObject.SetActive(false);
            }
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

            if (audioSource != null && sonidoComida != null)
                audioSource.PlayOneShot(sonidoComida);


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
    
    public void ReiniciarEstado()
    {
        tareaCepillarCompletada = false;
        tiempoSobreAreaCepillado = 0f;

        if (barraCepilladoUI != null)
        {
            barraCepilladoUI.value = 0f;
            barraCepilladoUI.gameObject.SetActive(false);
        }

        tareaAlimentarCompletada = false;

        Image platitoImage = platitoUI.GetComponent<Image>();
        if (platitoImage != null && platitoVacioSprite != null)
        {
            platitoImage.sprite = platitoVacioSprite;
        }
    }
}