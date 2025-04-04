using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
  [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    public string[] GetDialogueLines()
    {
        return dialogueLines;
    }
}
