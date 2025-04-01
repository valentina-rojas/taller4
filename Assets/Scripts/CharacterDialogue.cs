using UnityEngine;

public class CharacterDialogue : MonoBehaviour
{
  [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    public string[] GetDialogueLines()
    {
        return dialogueLines;
    }
}
