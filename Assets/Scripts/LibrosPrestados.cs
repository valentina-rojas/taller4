using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LibrosPrestadosManager : MonoBehaviour
{
    private UIManager uiManager;

    private void Start()
    {
        uiManager = FindFirstObjectByType<UIManager>();

        if (uiManager != null)
        {
            if (uiManager.GetBotonCerrarLibros() != null)
                uiManager.GetBotonCerrarLibros().onClick.AddListener(CerrarLibrosPrestados);

            if (uiManager.GetBotonAbrirLibros() != null)
                uiManager.GetBotonAbrirLibros().onClick.AddListener(MostrarLibrosPrestados);

            uiManager.GetPanelLibrosPrestados().SetActive(false);
        }
    }

    public void MostrarLibrosPrestados()
    {
        if (uiManager == null) return;

        if (uiManager.GetPanelHistorial().activeSelf)
        {
            Debug.Log("Primero cerrá el panel de historial.");
            return;
        }

        var personajes = CharacterManager.instance.GetPersonajesAtendidos();

        LimpiarLibrosUI();

        if (personajes == null || personajes.Count == 0)
        {
            MostrarMensajeLibrosVacio("No hay libros prestados para mostrar.");
        }
        else
        {
            foreach (var personaje in personajes)
            {
                if (!string.IsNullOrEmpty(personaje.tituloLibroPrestado))
                {
                    GameObject entrada = Instantiate(uiManager.GetPrefabEntradaLibro(), uiManager.GetLibrosContent());
                    TMP_Text[] textos = entrada.GetComponentsInChildren<TMP_Text>();

                    if (textos.Length >= 2)
                    {
                        textos[0].text = personaje.nombreDelCliente;
                        textos[1].text = personaje.tituloLibroPrestado;
                    }
                    else
                    {
                        Debug.LogWarning("Prefab de entrada libros necesita al menos 2 TMP_Text");
                    }
                }
            }
        }

        uiManager.GetPanelLibrosPrestados().SetActive(true);
    }

    private void LimpiarLibrosUI()
    {
        foreach (Transform child in uiManager.GetLibrosContent())
        {
            Destroy(child.gameObject);
        }
    }

    private void MostrarMensajeLibrosVacio(string mensaje)
    {
        GameObject entradaVacia = Instantiate(uiManager.GetPrefabEntradaLibro(), uiManager.GetLibrosContent());
        TMP_Text[] textos = entradaVacia.GetComponentsInChildren<TMP_Text>();

        if (textos.Length >= 2)
        {
            textos[0].text = mensaje;
            textos[1].text = "";
        }
    }

    public void CerrarLibrosPrestados()
    {
        if (uiManager != null)
        {
            uiManager.GetPanelLibrosPrestados().SetActive(false);
        }
    }
}