using UnityEngine;
using System.Collections.Generic;

public class PlantManager : MonoBehaviour
{
    public static PlantManager instance;

    public int totalPlants = 0;
    private int fullyWateredPlants = 0;
    private List<PlantWithRegadera> plantas = new List<PlantWithRegadera>();

    private void Awake()
    {
        instance = this;
    }
    public int expectedPlantCount = 4;

    public void RegisterPlant(PlantWithRegadera planta)
    {
        plantas.Add(planta);
        totalPlants = plantas.Count;

        if (plantas.Count == expectedPlantCount)
        {
            Debug.Log("Todas las plantas registradas. Actualizando estado.");
            ActualizarEstadoPlantasPorTarea();
        }
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

    public void ReiniciarEstado()
    {
        fullyWateredPlants = 0;

        foreach (var planta in plantas)
        {
            planta.ReiniciarPlanta();
        }
    }

    public void ActualizarEstadoPlantasPorTarea()
    {
        foreach (var planta in plantas)
        {
            planta.ActualizarEstadoPorTareaActiva();
        }
    }

}