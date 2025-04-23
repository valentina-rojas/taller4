using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private UIManager uiManager;
    private CharacterSpawn characterSpawn;

    [Header("Estado del juego")]
    public CharacterAttributes personajeActual;

    public GameObject panelInfoLibro;
    public GameObject panelFinNivel;
    public TMP_Text textoDia;
    public int nivelActual = 1;

    public enum ResultadoRecomendacion { Ninguna, Buena, Mala }
    public ResultadoRecomendacion resultadoRecomendacion = ResultadoRecomendacion.Ninguna;

    public int recomendacionesBuenas = 0;
    public int recomendacionesMalas = 0;

    public TMP_Text textoResultadoFinal;
    public TMP_Text textoTituloFinDeDia;

    [System.Serializable]
    public class Nivel
    {
        public GameObject[] personajesDelNivel;
    }

    [Header("Niveles del juego")]
    public Nivel[] niveles;

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

        panelInfoLibro.SetActive(true);
        textoDia.text = $"Día {nivelActual}";

        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(3f);

        panelInfoLibro.SetActive(false);
        Time.timeScale = 1f;

        FindFirstObjectByType<CatDialogues>().IniciarDialogoDelDia(nivelActual);
    }

    public void IniciarSpawnDePersonajes()
    {

        TaskManager.instance.OcultarListaTareas();
        CameraManager.instance.DesactivarBotonCamara();

        if (nivelActual - 1 < niveles.Length)
        {
            characterSpawn.AsignarPersonajesDelNivel(niveles[nivelActual - 1].personajesDelNivel);
            characterSpawn.ComenzarSpawn();
        }
        else
        {
            Debug.LogWarning("No hay más niveles definidos.");
        }
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
            resultadoRecomendacion = ResultadoRecomendacion.Buena;
            recomendacionesBuenas++;
            //ReputationBar.instance.AplicarDecision("buena");
        }
        /*   else if (esDelTipoPreferido)
           {
               resultadoRecomendacion = ResultadoRecomendacion.Buena; // si querés que también sea buena, o poné "Neutra"
               ReputationBar.instance.AplicarDecision("neutra");
           }*/
        else
        {
            resultadoRecomendacion = ResultadoRecomendacion.Mala;
            recomendacionesMalas++;
            // ReputationBar.instance.AplicarDecision("mala");
        }
    }


    public void CompletarRestauracion()
    {
        Debug.Log("Restauración completada.");

        resultadoRecomendacion = ResultadoRecomendacion.Buena;

    }

    public void CompletarPortada()
    {
        Debug.Log("Portada completada.");
        CameraManager.instance.DesctivarPanelPortada();

        resultadoRecomendacion = ResultadoRecomendacion.Buena;

          if (characterSpawn != null)
        {
            characterSpawn.EndInteraction();
        }

    }




    public void FinDeNivel()
    {
        nivelActual++;

        panelFinNivel.gameObject.SetActive(true);

        textoTituloFinDeDia.text = $"Fin del Día {nivelActual - 1}";

        string mensajeFinal = "";

        if (recomendacionesBuenas > recomendacionesMalas)
        {
            mensajeFinal = "¡Buen trabajo! Tus recomendaciones ayudaron a muchos clientes.";
        }
        else if (recomendacionesMalas > recomendacionesBuenas)
        {
            mensajeFinal = "Hoy no fue el mejor día... ¡Seguro mañana será mejor!";
        }
        else
        {
            mensajeFinal = "Un día regular. ¡Seguro mañana será mejor!";
        }

        textoResultadoFinal.text = mensajeFinal;

        panelFinNivel.gameObject.SetActive(true);

        recomendacionesBuenas = 0;
        recomendacionesMalas = 0;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        nivelActual = 1;
        SceneManager.LoadScene("MenuPrincipal");
    }

}
