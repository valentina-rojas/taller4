using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Diálogo")]
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button botonSiguiente;

    [Header("Historial de Pedido")]
    [SerializeField] private GameObject panelHistorial;
    [SerializeField] private TMP_Text textoNombreCliente;
    [SerializeField] private TMP_Text textoDescripcionPedido;
    [SerializeField] private Button botonCerrarHistorial;
    [SerializeField] private Button botonAbrirHistorial; // botón con ícono de hoja

    // Getters para diálogo
    public GameObject GetDialogueMark() => dialogueMark;
    public GameObject GetDialoguePanel() => dialoguePanel;
    public TMP_Text GetDialogueText() => dialogueText;
    public Button GetBotonSiguiente() => botonSiguiente;

    // Getters para historial
    public GameObject GetPanelHistorial() => panelHistorial;
    public TMP_Text GetTextoNombreCliente() => textoNombreCliente;
    public TMP_Text GetTextoDescripcionPedido() => textoDescripcionPedido;
    public Button GetBotonCerrarHistorial() => botonCerrarHistorial;
    public Button GetBotonAbrirHistorial() => botonAbrirHistorial;
}

