using UnityEngine;
using TMPro; // Necesario para TMP_Text

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;


    public GameObject GetDialogueMark() => dialogueMark;
    public GameObject GetDialoguePanel() => dialoguePanel;
    public TMP_Text GetDialogueText() => dialogueText;


    
}
