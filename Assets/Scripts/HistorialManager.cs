using UnityEngine;

public class HistorialManager : MonoBehaviour
{
    private UIManager uiManager;
    private CharacterAttributes personajeActual;

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

    public void MostrarHistorial(CharacterAttributes personaje)
    {
        if (uiManager == null || personaje == null) return;

        personajeActual = personaje;

        uiManager.GetTextoNombreCliente().text = personaje.nombreDelCliente;
        uiManager.GetTextoDescripcionPedido().text = personaje.descripcionPedido;
        uiManager.GetPanelHistorial().SetActive(true);
    }

    public void AbrirHistorialDesdeBoton()
    {
        CharacterManager characterManager = CharacterManager.instance;

        if (characterManager == null || uiManager == null) return;

        personajeActual = characterManager.UltimoPersonajeAtendido;

        if (personajeActual == null) return;

        uiManager.GetTextoNombreCliente().text = personajeActual.nombreDelCliente;
        uiManager.GetTextoDescripcionPedido().text = personajeActual.descripcionPedido;
        uiManager.GetPanelHistorial().SetActive(true);
    }


    public void CerrarHistorial()
    {
        if (uiManager != null)
        {
            uiManager.GetPanelHistorial().SetActive(false);
        }
    }
}

