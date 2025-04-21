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

    /*  public void IniciarSpawnDePersonajes()
      {
          characterSpawn.ComenzarSpawn();
      }*/


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
            ReputationBar.instance.AplicarDecision("buena");
        }
        /*   else if (esDelTipoPreferido)
           {
               resultadoRecomendacion = ResultadoRecomendacion.Buena; // si querés que también sea buena, o poné "Neutra"
               ReputationBar.instance.AplicarDecision("neutra");
           }*/
        else
        {
            resultadoRecomendacion = ResultadoRecomendacion.Mala;
            ReputationBar.instance.AplicarDecision("mala");
        }
    }
    public void FinDeNivel()
    {
        Debug.Log("Fin de nivel alcanzado.");

        nivelActual++;

        panelFinNivel.gameObject.SetActive(true);
        // StartCoroutine(MostrarCartelInicioDia());
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        nivelActual = 1;
        SceneManager.LoadScene("MenuPrincipal");
    }

}
