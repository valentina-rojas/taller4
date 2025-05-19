using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    private Button botonSiguiente; 

    private float typingTime = 0.05f;
    private bool isMouseOver = false;
    private bool didDialogueStart;
    private int lineIndex;
    private bool hasInteracted = false;

    private string[] dialogueLines;
    private CharacterAttributes characterAttributes;

    private Coroutine typingCoroutine;
    private bool isTyping = false;

    private void Start()
    {
        UIManager uiManager = FindFirstObjectByType<UIManager>();

        if (uiManager != null)
        {
            dialogueMark = uiManager.GetDialogueMark();
            dialoguePanel = uiManager.GetDialoguePanel();
            dialogueText = uiManager.GetDialogueText();
            botonSiguiente = uiManager.GetBotonSiguiente(); 
        }
        else
        {
            Debug.LogError("UIManager no encontrado en la escena.");
        }

        if (botonSiguiente != null)
        {
            botonSiguiente.onClick.AddListener(NextDialogueLine);
            botonSiguiente.gameObject.SetActive(false); 
        }

        characterAttributes = GetComponent<CharacterAttributes>();
    }

    void Update()
    {
        if (isMouseOver && Input.GetMouseButtonDown(0) && !hasInteracted && !didDialogueStart)
        {
            StartDialogue();
        }
    }

    public void EmpezarDialogoResultado()
    {
        hasInteracted = false;
        StartDialogue();
    }

    private void StartDialogue()
    {
        if (characterAttributes == null) return;

        switch (GameManager.instance.resultadoRecomendacion)
        {
            case GameManager.ResultadoRecomendacion.Buena:
                dialogueLines = characterAttributes.GetDialogueBuena();
                break;
            case GameManager.ResultadoRecomendacion.Mala:
                dialogueLines = characterAttributes.GetDialogueMala();
                break;
            default:
                dialogueLines = characterAttributes.GetDialogueInicio();
                break;
        }

        if (dialogueLines == null || dialogueLines.Length == 0) return;

        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        CameraManager.instance.DesactivarBotonCamara();
        lineIndex = 0;
        typingCoroutine = StartCoroutine(ShowLine());

        if (botonSiguiente != null)
            botonSiguiente.gameObject.SetActive(true);
    }

    public void NextDialogueLine()
    {
        if (!didDialogueStart || dialogueLines == null) return;

        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = dialogueLines[lineIndex];
            isTyping = false;
            return;
        }

        lineIndex++;

        if (lineIndex < dialogueLines.Length)
        {
            typingCoroutine = StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(false);
            hasInteracted = true;

            if (botonSiguiente != null)
                botonSiguiente.gameObject.SetActive(false);

            CharacterManager characterManager = FindObjectOfType<CharacterManager>();
            if (characterManager != null && characterAttributes != null)
            {
                characterManager.AtenderPersonaje(characterAttributes);
            }
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

    public void EnableDialogue()
    {
        if (!hasInteracted)
        {
            isMouseOver = true;
            dialogueMark.SetActive(true);
        }
    }

    public bool HaTerminadoElDialogo()
    {
        return !didDialogueStart;
    }
}