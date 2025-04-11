using UnityEngine;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private UIManager uiManager;
    private CharacterSpawn characterSpawn;

    [Header("Estado del juego")]
    public CharacterAttributes personajeActual;

    public GameObject panelInfoLibro;
    public TMP_Text textoDia;
    public int numeroDia = 1; 

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

     StartCoroutine(MostrarCartelInicioDia());
    }

    private IEnumerator MostrarCartelInicioDia()
    {
        // Mostrar panel con texto del día
        panelInfoLibro.SetActive(true);
        textoDia.text = $"Día {numeroDia}";

        // Pausar juego (opcional) y desactivar interacción
        Time.timeScale = 0f;
        // También podrías desactivar inputs o pausar UI aquí

        // Esperar unos segundos en tiempo real (no afectado por Time.timeScale)
        yield return new WaitForSecondsRealtime(2.5f);

        // Ocultar panel y reanudar juego
        panelInfoLibro.SetActive(false);
        Time.timeScale = 1f;

      // Comienza el juego
      FindFirstObjectByType<CatDialogues>().IniciarDialogoDelDia(numeroDia); 
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
