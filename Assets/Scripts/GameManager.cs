using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private UIManager uiManager;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        uiManager = FindFirstObjectByType<UIManager>();

        if (uiManager == null)
        {
            Debug.LogError("UIManager no encontrado en la escena.");
        }
    }

    public void ConfirmarLibro(string titulo)
    {
      //Debug.Log("libro seleccionado" + titulo);
    }
}


