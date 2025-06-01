using UnityEngine;
using UnityEngine.UI; 
using System.Collections;

public class PlantWithRegadera : MonoBehaviour
{
    public Sprite[] growthStages;
    public SpriteRenderer plantRenderer;
    public GameObject regadera;
    public Slider barraRiegoUI; 

    [Header("Sonido")]
    public AudioSource audioSource;
    public AudioClip sonidoRegadera;


    private int waterClicks = 0;
    private int maxWater = 3;
    private bool regaderaVisible = false;
    private bool isFullyWatered = false;

    public static PlantWithRegadera instance;

    private Coroutine wateringCoroutine;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource.Stop();
        audioSource.loop = false;
        
        if (growthStages != null && growthStages.Length > 0)
        {
            plantRenderer.sprite = growthStages[0];
        }

        if (regadera != null)
            regadera.SetActive(false);

        if (barraRiegoUI != null)
        {
            barraRiegoUI.gameObject.SetActive(false); 
            barraRiegoUI.maxValue = maxWater;
            barraRiegoUI.value = 0;
        }

        if (PlantManager.instance != null)
            PlantManager.instance.RegisterPlant();
    }

    private void OnMouseDown()
    {
        if (isFullyWatered) return;

        ShowRegadera();

        if (wateringCoroutine == null)
            wateringCoroutine = StartCoroutine(WateringRoutine());
    }

    private void OnMouseUp()
    {
        if (wateringCoroutine != null)
        {
            StopCoroutine(wateringCoroutine);
            wateringCoroutine = null;
        }

        if (!isFullyWatered)
        {
            waterClicks = 0;
            UpdatePlantAppearance();

            if (barraRiegoUI != null)
            {
                barraRiegoUI.value = 0;
                barraRiegoUI.gameObject.SetActive(false);
            }
        }

        if (regaderaVisible)
        {
            regaderaVisible = false;
            if (regadera != null)
                regadera.SetActive(false);
        
            // Detener sonido si estaba sonando
            if (audioSource != null && audioSource.isPlaying)
            {
            audioSource.Stop();
            audioSource.loop = false;
            Debug.Log("🔇 Sonido de regadera detenido.");
            }
        }
    }

    private IEnumerator WateringRoutine()
    {
        while (!isFullyWatered)
        {
            WaterPlant();
            yield return new WaitForSeconds(2f);
        }
    }

    void ShowRegadera()
    {
        regaderaVisible = true;

        if (regadera != null)
            regadera.SetActive(true);

        if (barraRiegoUI != null)
            barraRiegoUI.gameObject.SetActive(true); 

        // ✅ Reproducir sonido solo si no está ya reproduciéndose
        if (audioSource != null && sonidoRegadera != null)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = sonidoRegadera;
                audioSource.loop = true;
                audioSource.Play();
                Debug.Log("💧 Sonido de regadera iniciado");
            }
        }   
    }

    void WaterPlant()
    {
        waterClicks++;
        UpdatePlantAppearance();

        if (barraRiegoUI != null)
            barraRiegoUI.value = waterClicks;

        if (waterClicks >= maxWater)
        {
            FinishWatering();
        }
    }

    void UpdatePlantAppearance()
    {
        int stage = Mathf.Clamp(waterClicks, 0, growthStages.Length - 1);
        plantRenderer.sprite = growthStages[stage];
    }

    void FinishWatering()
    {
        Debug.Log("¡La planta está florecida!");

        regaderaVisible = false;
        isFullyWatered = true;

        if (regadera != null)
            regadera.SetActive(false);

        if (barraRiegoUI != null)
            barraRiegoUI.gameObject.SetActive(false); 

        // ✅ Detener sonido de regadera si aún está activo
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.loop = false;
            Debug.Log("🔇 Sonido de regadera detenido al terminar riego.");
        }

        if (PlantManager.instance != null)
            PlantManager.instance.NotifyPlantFullyWatered();
    }

    private void AccionFinal()
    {
        Debug.Log("Plantas regadas");
        TaskManager.instance.CompletarTareaPorID(4);
    }
}
