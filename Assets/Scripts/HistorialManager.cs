using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class HistorialManager : MonoBehaviour
{
    private UIManager uiManager;

    private void Start()
    {
        uiManager = FindFirstObjectByType<UIManager>();

        if (uiManager != null)
        {
            if (uiManager.GetBotonCerrarHistorial() != null)
                uiManager.GetBotonCerrarHistorial().onClick.AddListener(CerrarHistorial);

            if (uiManager.GetBotonAbrirHistorial() != null)
                uiManager.GetBotonAbrirHistorial().onClick.AddListener(AbrirHistorialDesdeBoton);

            uiManager.GetPanelHistorial().SetActive(false); // Ocultar al inicio
        }
    }

    public void AbrirHistorialDesdeBoton()
    {
        if (uiManager == null) return;

        // Obtener la lista completa de personajes atendidos en el día
        List<CharacterAttributes> personajes = CharacterManager.instance.GetPersonajesAtendidos();

        LimpiarHistorialUI();

        if (personajes == null || personajes.Count == 0)
        {
            // Si no hay personajes atendidos, mostrar mensaje vacío
            MostrarMensajeHistorialVacio("No hay historial para mostrar.");
        }
        else
        {
            // Instanciar una entrada para cada personaje en el historial
            foreach (CharacterAttributes personaje in personajes)
            {
                GameObject entrada = Instantiate(uiManager.GetPrefabEntradaHistorial(), uiManager.GetHistorialContent());
                TMP_Text[] textos = entrada.GetComponentsInChildren<TMP_Text>();

                if (textos.Length >= 2)
                {
                    textos[0].text = personaje.nombreDelCliente;
                    textos[1].text = personaje.descripcionPedido;
                }
                else
                {
                    Debug.LogWarning("Prefab de entrada historial necesita al menos 2 TMP_Text");
                }
            }
        }

        uiManager.GetPanelHistorial().SetActive(true);
    }

    private void LimpiarHistorialUI()
    {
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

    public void CerrarHistorial()
    {
        if (uiManager != null)
        {
            uiManager.GetPanelHistorial().SetActive(false);
        }
    }
}


