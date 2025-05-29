using UnityEngine;
using System.Collections;


public class PlantWithRegadera : MonoBehaviour
{
    public Sprite[] growthStages;         // 4 estados de la planta (desde el sprite sheet)
    public SpriteRenderer plantRenderer;  // Renderer de la planta
    public GameObject regadera;           // Objeto visual de la regadera
    private int waterClicks = 0;
    private int maxWater = 3;
    private bool regaderaVisible = false;
    private bool isFullyWatered = false; // 🔒 Nueva variable de bloqueo

    public static PlantWithRegadera instance;

    private Coroutine wateringCoroutine;


     private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        if (growthStages != null && growthStages.Length > 0)
        {
            plantRenderer.sprite = growthStages[0];
        }
        else
        {
            Debug.LogError("No se han asignado sprites a growthStages.");
        }
        
        if (regadera != null)
            regadera.SetActive(false);         // Regadera oculta


             if (PlantManager.instance != null)
        PlantManager.instance.RegisterPlant();
    }

    private void OnMouseDown()
{
    if (isFullyWatered)
        return;

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
        waterClicks = 0; // 🔁 Reinicia el progreso de riego
        UpdatePlantAppearance(); // 🔄 Vuelve al sprite inicial
    }

    if (regaderaVisible)
    {
        regaderaVisible = false;
        if (regadera != null)
            regadera.SetActive(false);
    }
}


private IEnumerator WateringRoutine()
{
    while (!isFullyWatered)
    {
        WaterPlant();
        yield return new WaitForSeconds(2f); // espera 1 segundo entre riegos
    }
}



    void ShowRegadera()
    {
        regaderaVisible = true;
        if (regadera != null)
            regadera.SetActive(true);
    }

    void WaterPlant()
    {
        waterClicks++;
        UpdatePlantAppearance();

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
        // Planta completamente regada, podrías agregar una animación o partículas aquí
        Debug.Log("¡La planta está florecida!");

        regaderaVisible = false;
         isFullyWatered = true; // ✅ Bloquear futuros clics

        if (regadera != null)
            regadera.SetActive(false); // 🚿 Ocultamos la regadera

            if (PlantManager.instance != null)
    PlantManager.instance.NotifyPlantFullyWatered();

    }

     private void AccionFinal()
    {
        Debug.Log("Plantas regadas");
        TaskManager.instance.CompletarTareaPorID(4);
    }
}