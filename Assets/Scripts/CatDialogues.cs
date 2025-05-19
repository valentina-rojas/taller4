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

    private float typingTime = 0.05f;

    private bool didDialogueStart;
    private bool isTyping;
    private Coroutine typingCoroutine;

    private int lineIndex;
    private string[] dialogueLines;

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
            botonSiguiente.onClick.AddListener(AvanzarDialogo);
    }

    public void IniciarDialogoDelDia(int dia)
    {
        if (dialoguesPorDia.ContainsKey(dia))
        {
            dialogueLines = dialoguesPorDia[dia];
            StartDialogue();
        }
        else
        {
            Debug.LogWarning("No hay diálogo asignado para el día: " + dia);
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        typingCoroutine = StartCoroutine(ShowLine());
    }

    public void AvanzarDialogo()
    {
        if (!didDialogueStart) return;

        if (isTyping)
        {
            MostrarLineaCompleta();
        }
        else
        {
            NextDialogueLine();
        }
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
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            CameraManager.instance.ActivarBotonCamara();
            TaskManager.instance.MostrarTareas();
        }
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
    }

    private void MostrarLineaCompleta()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        dialogueText.text = dialogueLines[lineIndex];
        isTyping = false;
    }
}