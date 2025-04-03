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

    public void MostrarInformacion(BookData libro)
    {
        libroActual = libro; // Guardamos el libro actual

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
    }

    public void CancelarSeleccion()
    {
        panelInfoLibro.SetActive(false);
    }
}
