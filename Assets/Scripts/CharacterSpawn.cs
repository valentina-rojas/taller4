using UnityEngine;
using System.Collections;

public class CharacterSpawn : MonoBehaviour
{
    public GameObject[] characters;  
    public Transform spawnPoint;
    public Transform destination;    

    private int currentIndex = 0;    

    void Start()
    {
        StartCoroutine(SpawnCharacters());
    }

    IEnumerator SpawnCharacters()
    {
        while (currentIndex < characters.Length)
        {
            GameObject currentCharacter = Instantiate(characters[currentIndex], spawnPoint.position, Quaternion.identity);
            yield return StartCoroutine(MoveCharacter(currentCharacter, destination.position));
            yield return new WaitUntil(() => InteractionCompleted());
            Destroy(currentCharacter);
            currentIndex++;
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("Todos los personajes han pasado.");
    }

    IEnumerator MoveCharacter(GameObject character, Vector3 targetPosition)
    {
        float duration = 2f;
        float elapsedTime = 0f;
        Vector3 startPosition = character.transform.position;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            character.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            yield return null;
        }

        character.transform.position = targetPosition;

        HabilitarDialogo();
    }

    bool InteractionCompleted()
    {
        return Input.GetKeyDown(KeyCode.Space); 
    }

    private void HabilitarDialogo()
    {
        DialogueManager dialogueManager = FindFirstObjectByType<DialogueManager>();
        if (dialogueManager != null)
        {
            dialogueManager.EnableDialogue();
            Debug.Log("Dialogue habilitado.");
        }
        else
        {
            Debug.LogError("DialogueManager no encontrado al habilitar diálogo.");
        }
    }

}
