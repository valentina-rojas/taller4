using UnityEngine;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;

    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    private float typingTime = 0.05f;
    private bool isMouseOver = false;
    private bool isPlayerInScene;
    private bool startDialogue;
    private bool didDialogueStart;
    private int lineIndex;
    private bool hasInteracted = false;


    private void Start()
    {
      
        UIManager uiManager = FindFirstObjectByType<UIManager>();

        if (uiManager != null)
        {
            dialogueMark = uiManager.GetDialogueMark();
            dialoguePanel = uiManager.GetDialoguePanel();
            dialogueText = uiManager.GetDialogueText();
        }
        else
        {
            Debug.LogError("UIManager no encontrado en la escena.");
        }
    }


    void Update()
    {

        if (isMouseOver && Input.GetMouseButtonDown(0) && !hasInteracted) 
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }

        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(false);
            hasInteracted = true;
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }

    private void OnMouseEnter()
    {
        if (!hasInteracted)
        {
            isMouseOver = true;
            dialogueMark.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
        if (!hasInteracted)
        {
            dialogueMark.SetActive(false);
        }
    }
}
