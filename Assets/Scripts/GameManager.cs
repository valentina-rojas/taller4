using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private UIManager uiManager;
    private CharacterSpawn characterSpawn;

    [Header("Estado del juego")]
    public CharacterAttributes personajeActual;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        uiManager = FindFirstObjectByType<UIManager>();
        characterSpawn = FindFirstObjectByType<CharacterSpawn>();

        if (uiManager == null)
            Debug.LogError("UIManager no encontrado en la escena.");

        if (characterSpawn == null)
            Debug.LogError("CharacterSpawn no encontrado en la escena.");
    }



    public void IniciarSpawnDePersonajes()
    {
        characterSpawn.ComenzarSpawn();
    }

    public void EstablecerPersonajeActual(CharacterAttributes personaje)
    {
        personajeActual = personaje;
    }

    public void VerificarRecomendacion(BookData libro)
    {
        if (personajeActual == null)
        {
            Debug.LogError("No hay personaje actual asignado.");
            return;
        }

        bool esCorrecto = personajeActual.libroDeseadoID == libro.libroID;
        bool esDelTipoPreferido = personajeActual.tipoPreferido == libro.tipoLibro;

        if (esCorrecto)
        {
            Debug.Log("¡Recomendación correcta! Era el libro exacto que quería.");
            ReputationBar.instance.AplicarDecision("buena");
        }
        else if (personajeActual.tipoPreferido == libro.tipoLibro)
        {
            Debug.Log("Buena elección. Es del tipo que le gusta, aunque no era el libro exacto.");
            ReputationBar.instance.AplicarDecision("neutra");
        }
        else
        {
            Debug.Log("Mala recomendación. No coincide ni con el tipo ni el libro deseado.");
            ReputationBar.instance.AplicarDecision("mala");
        }
    }
}
