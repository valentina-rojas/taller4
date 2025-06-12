using UnityEngine;
using TMPro;

public class HistorialManager : MonoBehaviour
{
    private UIManager uiManager;
    private GameObject ultimaEntradaInstanciada;

    private void Start()
    {
        uiManager = FindFirstObjectByType<UIManager>();

        if (uiManager != null)
        {
            if (uiManager.GetBotonCerrarHistorial() != null)
                uiManager.GetBotonCerrarHistorial().onClick.AddListener(CerrarHistorial);

            if (uiManager.GetBotonAbrirHistorial() != null)
                uiManager.GetBotonAbrirHistorial().onClick.AddListener(AbrirHistorialDesdeBoton);

            uiManager.GetPanelHistorial().SetActive(false); 
        }
    }

    public void AbrirHistorialDesdeBoton()
    {
        if (uiManager == null) return;

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

                    ultimaEntradaInstanciada = entrada;
                    Debug.Log($"Entrada añadida al historial: {personaje.nombreDelCliente}");
                }
                else
                {
                    Debug.LogWarning("El prefab necesita al menos 2 TMP_Text.");
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

        ultimaEntradaInstanciada = null;
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

    public void TacharUltimaEntrada()
    {
        if (ultimaEntradaInstanciada == null)
        {
            Debug.LogWarning("No hay entrada instanciada para tachar.");
            return;
        }

        TMP_Text[] textos = ultimaEntradaInstanciada.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text txt in textos)
        {
            string textoOriginal = txt.text;
            txt.text = $"<color=red><s>{textoOriginal}</s></color>";

            Debug.Log($"Texto antes: {textoOriginal}");
            Debug.Log($"Texto después: {txt.text}");
        }

        Canvas.ForceUpdateCanvases();
        Debug.Log("Entrada tachada correctamente.");
    }
}