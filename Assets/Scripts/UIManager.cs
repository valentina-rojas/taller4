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
    [SerializeField] private TMP_Text botonSiguienteTexto;
    [SerializeField] private Button botonRepetir;

    [Header("Historial de Pedido")]
    [SerializeField] private Transform historialContent; 
    [SerializeField] private GameObject prefabEntradaHistorial;
    [SerializeField] private GameObject panelHistorial;
    [SerializeField] private Button botonCerrarHistorial;
    [SerializeField] private Button botonAbrirHistorial; 

    public GameObject GetDialogueMark() => dialogueMark;
    public GameObject GetDialoguePanel() => dialoguePanel;
    public TMP_Text GetDialogueText() => dialogueText;
    public Button GetBotonSiguiente() => botonSiguiente;
    public TMP_Text GetBotonSiguienteTexto() => botonSiguienteTexto;
    public Button GetBotonRepetir() => botonRepetir;

    public Transform GetHistorialContent() => historialContent;
    public GameObject GetPrefabEntradaHistorial() => prefabEntradaHistorial;
    public GameObject GetPanelHistorial() => panelHistorial;
    public Button GetBotonCerrarHistorial() => botonCerrarHistorial;
    public Button GetBotonAbrirHistorial() => botonAbrirHistorial;
}

