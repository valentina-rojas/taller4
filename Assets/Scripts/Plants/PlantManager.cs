using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public static PlantManager instance;

    public int totalPlants = 0;
    private int fullyWateredPlants = 0;

    private void Awake()
    {
        instance = this;
    }

    public void RegisterPlant()
    {
        totalPlants++;
    }

    public void NotifyPlantFullyWatered()
    {
        fullyWateredPlants++;

        if (fullyWateredPlants >= totalPlants)
        {
            Debug.Log("¡Todas las plantas fueron regadas!");
            AccionFinal();
        }
    }

    private void AccionFinal()
    {
        Debug.Log("Acción final ejecutada: todas las plantas regadas.");
        TaskManager.instance.CompletarTareaPorID(4);
    }
}
