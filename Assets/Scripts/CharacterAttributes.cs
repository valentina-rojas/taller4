using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{

  [Header("Dialogos del personaje")]
  [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

  [Header("Preferencias del personaje")]
  public int libroDeseadoID;
  public string tipoPreferido;

  public string[] GetDialogueLines()
  {
    return dialogueLines;
  }
}
