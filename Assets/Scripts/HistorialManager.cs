using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HistorialManager : MonoBehaviour
{
    private UIManager uiManager;

    private void Start()
    {
        uiManager = FindFirstObjectByType<UIManager>();

        if (uiManager != null)
        {
            if (uiManager.GetBotonCerrarHistorial() != null)
                uiManager.GetBotonCerrarHistorial().onClick.AddListener(CerrarTodo);

            if (uiManager.GetBotonAbrirHistorial() != null)
                uiManager.GetBotonAbrirHistorial().onClick.AddListener(AbrirTodo);

            uiManager.GetPanelHistorial().SetActive(false);
        }
    }

    public void AbrirTodo()
    {
        if (uiManager == null) return;

        MostrarHistorial();
        MostrarLibrosPrestados();
        uiManager.GetPanelHistorial().SetActive(true);
    }

    public void CerrarTodo()
    {
        if (uiManager != null && uiManager.GetPanelHistorial() != null)
        {
            uiManager.GetPanelHistorial().SetActive(false);
            Debug.Log("Panel de historial cerrado"); // Mensaje de depuración
        }
    }

    #region Historial de Pedidos
    private void MostrarHistorial()
    {
        var personajes = CharacterManager.instance.GetPersonajesAtendidos();
        LimpiarHistorialUI();

        if (personajes == null || personajes.Count == 0)
        {
            MostrarMensajeHistorialVacio("No hay historial para mostrar.");
        }
        else
        {
            foreach (var personaje in personajes)
            {
                GameObject entrada = Instantiate(uiManager.GetPrefabEntradaHistorial(), uiManager.GetHistorialContent());
                TMP_Text[] textos = entrada.GetComponentsInChildren<TMP_Text>();

                if (textos.Length >= 2)
                {
                    textos[0].text = personaje.nombreDelCliente;
                    textos[1].text = personaje.descripcionPedido;
                }
            }
        }
    }

    private void LimpiarHistorialUI()
    {
        if (uiManager.GetHistorialContent() == null) return;

        foreach (Transform child in uiManager.GetHistorialContent())
        {
            Destroy(child.gameObject);
        }
    }

    private void MostrarMensajeHistorialVacio(string mensaje)
    {
        GameObject entradaVacia = Instantiate(uiManager.GetPrefabEntradaHistorial(), uiManager.GetHistorialContent());
        TMP_Text[] textos = entradaVacia.GetComponentsInChildren<TMP_Text>();

        if (textos.Length >= 2)
        {
            textos[0].text = mensaje;
            textos[1].text = "";
        }
    }
    #endregion

    #region Libros Prestados
    private void MostrarLibrosPrestados()
    {
        var personajes = CharacterManager.instance.GetPersonajesAtendidos();
        LimpiarLibrosUI();

        if (personajes == null || personajes.Count == 0)
        {
            MostrarMensajeLibrosVacio("No hay libros prestados para mostrar.");
            Debug.Log("No hay personajes atendidos"); // Mensaje de depuración
        }
        else
        {
            bool hayLibros = false;

            foreach (var personaje in personajes)
            {
                Debug.Log($"Revisando personaje: {personaje.nombreDelCliente} - Libro: '{personaje.tituloLibroPrestado}'");

                if (!string.IsNullOrEmpty(personaje.tituloLibroPrestado))
                {
                    hayLibros = true;

                    GameObject entrada = Instantiate(uiManager.GetPrefabEntradaLibro(), uiManager.GetLibrosContent());
                    TMP_Text[] textos = entrada.GetComponentsInChildren<TMP_Text>();

                    if (textos.Length >= 2)
                    {
                        textos[0].text = personaje.nombreDelCliente;
                        textos[1].text = personaje.tituloLibroPrestado;
                        Debug.Log($"Libro prestado mostrado: {personaje.tituloLibroPrestado}");
                    }
                    else
                    {
                        Debug.LogWarning("Prefab de entrada libro necesita al menos 2 TMP_Text.");
                    }
                }
            }

            if (!hayLibros)
            {
                MostrarMensajeLibrosVacio("No hay libros prestados para mostrar.");
                Debug.Log("No se encontraron libros prestados en personajes.");
            }
        }
    }


    private void LimpiarLibrosUI()
    {
        if (uiManager.GetLibrosContent() == null) return;

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
            Debug.Log("Mostrando mensaje de libros vacío"); // Mensaje de depuración
        }
    }
    #endregion
}