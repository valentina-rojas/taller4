using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
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

    private bool intentoFinPendiente = false;
    private bool revisandoIntento = false;

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
            libro.gameObject.SetActive(false);
        }
        else
        {
            resultadoRecomendacion = ResultadoRecomendacion.Mala;
            recomendacionesMalas++;
        }
    }

    public void CompletarRestauracion()
    {
        Debug.Log("Restauración completada.");
        resultadoRecomendacion = ResultadoRecomendacion.Buena;
        recomendacionesBuenas++;
    }

    public void CompletarPortada(List<StickerID> stickersUsados)
    {
        Debug.Log("Portada completada.");

        if (personajeActual == null)
        {
            Debug.LogError("No hay personaje actual asignado para comparar stickers.");
            return;
        }

        List<StickerID> stickersRequeridos = personajeActual.stickersRequeridos;

        Debug.Log($"Stickers requeridos ({stickersRequeridos.Count}): {string.Join(", ", stickersRequeridos)}");
        Debug.Log($"Stickers usados ({stickersUsados.Count}): {string.Join(", ", stickersUsados)}");

        bool tieneTodos = true;

        foreach (StickerID requerido in stickersRequeridos)
        {
            if (!stickersUsados.Contains(requerido))
            {
                Debug.LogWarning($"Falta sticker requerido: {requerido}");
                tieneTodos = false;
                break;
            }
            else
            {
                Debug.Log($"Sticker requerido presente: {requerido}");
            }
        }

        resultadoRecomendacion = tieneTodos ? ResultadoRecomendacion.Buena : ResultadoRecomendacion.Mala;

        if (tieneTodos)
            recomendacionesBuenas++;
        else
            recomendacionesMalas++;

        Debug.Log("Resultado recomendación: " + resultadoRecomendacion);

        CameraManager.instance.DesctivarPanelPortada();

        if (characterSpawn != null)
        {
            characterSpawn.EndInteraction();
        }
    }


    public void CompletarHechizo(CharacterAttributes.Hechizo hechizoRealizado)
    {
        if (personajeActual == null)
        {
            Debug.LogError("No hay personaje actual asignado.");
            return;
        }

        if (hechizoRealizado == personajeActual.hechizoSolicitado)
        {
            resultadoRecomendacion = ResultadoRecomendacion.Buena;
            recomendacionesBuenas++;
            Debug.Log($"Hechizo completado correctamente: {hechizoRealizado}");
        }
        else
        {
            resultadoRecomendacion = ResultadoRecomendacion.Mala;
            recomendacionesMalas++;
            Debug.LogWarning($"Hechizo incorrecto. Realizado: {hechizoRealizado}, Solicitado: {personajeActual.hechizoSolicitado}");
        }

        CameraManager.instance.DesctivarPanelHechizo();

        if (characterSpawn != null)
        {
            characterSpawn.EndInteraction();
        }

    }


     public void CompletarTrivia(int correctas, int incorrectas)
    {
        if (correctas > incorrectas)
        {
            resultadoRecomendacion = ResultadoRecomendacion.Buena;
            recomendacionesBuenas++;
        }
        else if (incorrectas > correctas)
        {
            resultadoRecomendacion = ResultadoRecomendacion.Mala;
            recomendacionesMalas++;
        }       


            if (characterSpawn != null)
        {
            characterSpawn.EndInteraction();
        }
     
    }


    public void FinDeNivel()
    {
        if (!TaskManager.instance.TodasLasTareasCompletadas())
        {
            FindFirstObjectByType<CatDialogues>()?.IniciarDialogoExtra("Aún quedan cosas por hacer... mejor termina tu lista de tareas.");
            intentoFinPendiente = true;

            if (!revisandoIntento)
                StartCoroutine(RevisarIntentoFinPendiente());

            return;
        }

        intentoFinPendiente = false;
        TaskManager.instance.OcultarListaTareas();
        nivelActual++;
        panelFinNivel.gameObject.SetActive(true);
        textoTituloFinDeDia.text = $"Fin del Día {nivelActual - 1}";

        string mensajeFinal = "";

        string resumenClientes = $"Clientes satisfechos: {recomendacionesBuenas}\nClientes insatisfechos: {recomendacionesMalas}\n";

        if (recomendacionesBuenas > recomendacionesMalas)
        {
            mensajeFinal = "¡Buen trabajo! Tus recomendaciones ayudaron a muchos clientes.\n\n" + resumenClientes;
        }
        else if (recomendacionesMalas > recomendacionesBuenas)
        {
            mensajeFinal = "Hoy no fue el mejor día... ¡Seguro mañana será mejor!\n\n" + resumenClientes;
        }
        else
        {
            mensajeFinal = "Un día regular. ¡Seguro mañana será mejor!\n\n" + resumenClientes;
        }


        textoResultadoFinal.text = mensajeFinal;

        recomendacionesBuenas = 0;
        recomendacionesMalas = 0;
    }

    private IEnumerator RevisarIntentoFinPendiente()
    {
        revisandoIntento = true;

        while (intentoFinPendiente)
        {
            yield return new WaitForSeconds(1f);

            if (TaskManager.instance.TodasLasTareasCompletadas())
            {
                FinDeNivel(); 
                break;
            }
        }

        revisandoIntento = false;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        nivelActual = 1;
        SceneManager.LoadScene("MenuPrincipal");
    }
}