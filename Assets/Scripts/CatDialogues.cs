using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class CatDialogues : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button botonSiguiente;
    [SerializeField] private TMP_Text botonSiguienteTexto;
    [SerializeField] private Button botonRepetir; 

    private float typingTime = 0.05f;
    private bool isTyping;
    private Coroutine typingCoroutine;
    private int lineIndex;
    private string[] dialogueLines;
    private int diaActual;

    private Dictionary<int, string[]> dialoguesPorDia = new Dictionary<int, string[]>()
    {
        { 1, new string[] {
            "¡Hola! Soy Minino, tu asistente en esta librería mágica.",
            "Cada día vendrán criaturas distintas buscando libros.",
            "Recomiéndales libros según sus gustos o el que buscan exactamente.",
            "Pero antes de recibir clientes deberías limpiar un poco este lugar...",
            "¡Suerte en tu primer día!" }
        },
        { 2, new string[] {
            "¡Buen trabajo ayer!",
            "Hoy los clientes serán un poco más exigentes.",
            "Recuerda: si no das el libro correcto, la reputación baja."
        }},
        { 3, new string[] {
            "¡Ya eres todo un experto!",
            "Hoy puede que algunos clientes no sepan lo que quieren...",
            "¡Confía en tu instinto de librero mágico!"
        }}
    };

    void Start()
    {
        if (botonSiguiente != null)
        {
            botonSiguiente.onClick.AddListener(OnBotonSiguienteClick);
            botonSiguiente.gameObject.SetActive(false);
        }

        if (botonRepetir != null)
        {
            botonRepetir.onClick.AddListener(OnBotonRepetirClick);
            botonRepetir.gameObject.SetActive(false);
        }
    }

    private void OnBotonSiguienteClick()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = dialogueLines[lineIndex];
            isTyping = false;

            ActualizarTextoBoton();
        }
        else
        {
            NextDialogueLine();
        }
    }

    private void OnBotonRepetirClick()
    {
        StartDialogue(); 
    }

    public void IniciarDialogoDelDia(int dia)
    {
        if (dialoguesPorDia.ContainsKey(dia))
        {
            diaActual = dia;
            dialogueLines = dialoguesPorDia[dia];
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        lineIndex = 0;

        if (botonSiguiente != null)
            botonSiguiente.gameObject.SetActive(true);

        if (botonRepetir != null)
            botonRepetir.gameObject.SetActive(false); 

        ActualizarTextoBoton();
        typingCoroutine = StartCoroutine(ShowLine());
    }

    private IEnumerator ShowLine()
    {
        isTyping = true;
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }

        isTyping = false;
        ActualizarTextoBoton();
    }

    private void NextDialogueLine()
    {
        lineIndex++;

        if (lineIndex < dialogueLines.Length)
        {
            typingCoroutine = StartCoroutine(ShowLine());
        }
        else
        {
            FinalizarDialogo();
        }
    }

    private void FinalizarDialogo()
    {
        dialoguePanel.SetActive(false);

        if (botonSiguiente != null)
            botonSiguiente.gameObject.SetActive(false);

        if (botonRepetir != null)
            botonRepetir.gameObject.SetActive(false); 

        CameraManager.instance?.ActivarBotonCamara();
        TaskManager.instance?.MostrarTareas();
    }

    private void ActualizarTextoBoton()
    {
        if (botonSiguienteTexto == null) return;

        bool esUltimaLinea = (lineIndex == dialogueLines.Length - 1);

        if (esUltimaLinea && !isTyping)
        {
            botonSiguienteTexto.text = "Finalizar";

            if (botonRepetir != null)
                botonRepetir.gameObject.SetActive(true); 
        }
        else
        {
            botonSiguienteTexto.text = "Siguiente";

            if (botonRepetir != null)
                botonRepetir.gameObject.SetActive(false);
        }
    }
}