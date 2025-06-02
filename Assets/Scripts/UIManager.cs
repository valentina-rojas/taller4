using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Diálogo")]
    [SerializeField] private Button dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button botonSiguiente;
    [SerializeField] private TMP_Text botonSiguienteTexto;
    [SerializeField] private Button botonRepetir;
    [SerializeField] private Button botonFinalizar;
    public Button GetDialogueMark() => dialogueMark;
    public GameObject GetDialoguePanel() => dialoguePanel;
    public TMP_Text GetDialogueText() => dialogueText;
    public Button GetBotonSiguiente() => botonSiguiente;
    public TMP_Text GetBotonSiguienteTexto() => botonSiguienteTexto;
    public Button GetBotonRepetir() => botonRepetir;
    public Button GetBotonFinalizar() => botonFinalizar;

    [Header("Historial de Pedido")]
    [SerializeField] private Transform historialContent;
    [SerializeField] private GameObject prefabEntradaHistorial;
    [SerializeField] private GameObject panelHistorial;
    [SerializeField] private Button botonCerrarHistorial;
    [SerializeField] private Button botonAbrirHistorial;
    public Transform GetHistorialContent() => historialContent;
    public GameObject GetPrefabEntradaHistorial() => prefabEntradaHistorial;
    public GameObject GetPanelHistorial() => panelHistorial;
    public Button GetBotonCerrarHistorial() => botonCerrarHistorial;
    public Button GetBotonAbrirHistorial() => botonAbrirHistorial;

    [Header("Libros Prestados")]
    [SerializeField] private GameObject panelLibrosPrestados;
    [SerializeField] private Transform librosContent;
    [SerializeField] private GameObject prefabEntradaLibro;
    [SerializeField] private Button botonCerrarLibros;
    [SerializeField] private Button botonAbrirLibros;
    public GameObject GetPanelLibrosPrestados() => panelLibrosPrestados;
    public Transform GetLibrosContent() => librosContent;
    public GameObject GetPrefabEntradaLibro() => prefabEntradaLibro;
    public Button GetBotonCerrarLibros() => botonCerrarLibros;
    public Button GetBotonAbrirLibros() => botonAbrirLibros;
}