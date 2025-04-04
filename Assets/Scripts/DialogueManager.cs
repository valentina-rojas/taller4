using UnityEngine;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;


    private float typingTime = 0.05f;
    private bool isMouseOver = false;
    private bool isPlayerInScene;
    private bool startDialogue;
    private bool didDialogueStart;
    private int lineIndex;
    private bool hasInteracted = false;
    private bool canStartDialogue = false;

  private string[] dialogueLines; 
    private CharacterAttributes characterAttributes;

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


       
        characterAttributes = GetComponent<CharacterAttributes>();
        if (characterAttributes != null)
        {
            dialogueLines = characterAttributes.GetDialogueLines();
        }
        else
        {
            Debug.LogWarning("CharacterAttributes no está asignado en este GameObject.");
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
          if (dialogueLines == null || dialogueLines.Length == 0) return;
          
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
        if (!hasInteracted && canStartDialogue)
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

    public void EnableDialogue()
    {
        

         if (!hasInteracted )
        {
            canStartDialogue = true;
            isMouseOver = true;
            dialogueMark.SetActive(true);
        }
    }


}
