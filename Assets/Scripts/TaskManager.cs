using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;

    public GameObject panelTareas;
    public Button botonAbrirTienda;

    public List<TMP_Text> textosTareas;
    public List<bool> tareasCompletadas;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        panelTareas.SetActive(false);

        tareasCompletadas = new List<bool>();
        for (int i = 0; i < textosTareas.Count; i++)
        {
            tareasCompletadas.Add(false);
        }
    }

    public void MostrarTareas()
    {
        panelTareas.SetActive(true);
    }


   public void OcultarListaTareas()
    {
        panelTareas.SetActive(false);
    }


    public void CompletarTareaPorID(int id)
    {
        if (id < 0 || id >= tareasCompletadas.Count)
        {
            Debug.LogWarning("ID de tarea inválido: " + id);
            return;
        }

        if (!tareasCompletadas[id])
        {
            tareasCompletadas[id] = true;
            string nombreOriginal = textosTareas[id].text;
            textosTareas[id].text = "<s>" + nombreOriginal + "</s>";
        }

        RevisarTareas();
    }
 
    private void RevisarTareas()
    {
        for (int i = 0; i < tareasCompletadas.Count; i++)
        {
            if (!tareasCompletadas[i])
                return;
        }

       botonAbrirTienda.gameObject.SetActive(true);

    }
}
