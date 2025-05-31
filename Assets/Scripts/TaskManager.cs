using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;

    public GameObject panelTareas;
    public Button botonAbrirLista;
    public Button botonCerrarLista;
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
        botonAbrirLista.gameObject.SetActive(false);
        botonCerrarLista.gameObject.SetActive(false);

        tareasCompletadas = new List<bool>();
        for (int i = 0; i < textosTareas.Count; i++)
        {
            tareasCompletadas.Add(false);
        }

        botonAbrirLista.onClick.AddListener(MostrarTareas);
        botonCerrarLista.onClick.AddListener(OcultarListaTareas);
        botonAbrirTienda.onClick.AddListener(OnClickAbrirTienda);
    }

    public void MostrarTareas()
    {
        panelTareas.SetActive(true);
        botonAbrirLista.gameObject.SetActive(false);
        botonCerrarLista.gameObject.SetActive(true);
    }

    public void OcultarListaTareas()
    {
        panelTareas.SetActive(false);
        botonAbrirLista.gameObject.SetActive(true);
        botonCerrarLista.gameObject.SetActive(false);
    }

    public void OcultarBotonTareas()
    {
        panelTareas.SetActive(false);
        botonAbrirLista.gameObject.SetActive(false);
    }

    private void OnClickAbrirTienda()
    {
        Debug.Log("Tienda abierta");
        botonAbrirTienda.gameObject.SetActive(false);
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
    }
    public bool TodasLasTareasCompletadas()
    {
        foreach (bool tarea in tareasCompletadas)
        {
            if (!tarea)
                return false;
        }
        return true;
    }

}