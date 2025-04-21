using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookManager : MonoBehaviour
{
    public static BookManager instance;

    public GameObject panelInfoLibro;
    public TMP_Text tituloTexto;
    public TMP_Text descripcionTexto;
    public Image imagenLibroUI;

    private BookData libroActual;

    public GameObject panelConfirmarSeleccion;
    public Image imagenConfirmarSeleccion;
    public TMP_Text tituloConfirmarSeleccion;

    private CharacterSpawn characterSpawn;

    public Button botonConfirmar;


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

        characterSpawn = FindFirstObjectByType<CharacterSpawn>();
        if (characterSpawn == null)
        {
            Debug.LogError("CharacterSpawn no encontrado por BookManager.");
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panelInfoLibro.activeSelf)
            {
                CancelarSeleccion();
            }
        }
    }


    public void MostrarInformacion(BookData libro)
    {
        libroActual = libro;

        panelInfoLibro.SetActive(true);
        tituloTexto.text = libro.titulo;
        descripcionTexto.text = libro.descripcion;
        imagenLibroUI.sprite = libro.imagenLibro;
    }

    public void ConfirmarSeleccion()
    {
        if (libroActual == null)
        {
            Debug.LogError("No hay libro seleccionado.");
            return;
        }

        Debug.Log("Libro seleccionado: " + libroActual.titulo);
        panelInfoLibro.SetActive(false);

        panelConfirmarSeleccion.SetActive(true);
        imagenConfirmarSeleccion.sprite = libroActual.imagenLibro;
        tituloConfirmarSeleccion.text = libroActual.titulo;
 

    }

    public void HabilitarBotonConfirmacion()
    {
        botonConfirmar.gameObject.SetActive(true);
    }

    public void DeshabilitarBotonConfirmacion()
    {
        botonConfirmar.gameObject.SetActive(false);
    }

    public void CancelarSeleccion()
    {
        panelInfoLibro.SetActive(false);
    }

    public void RecomendarLibro()
    {
        panelConfirmarSeleccion.SetActive(false);
        GameManager.instance.VerificarRecomendacion(libroActual);

        if (characterSpawn != null)
        {
            characterSpawn.EndInteraction();
        }

    }
}
