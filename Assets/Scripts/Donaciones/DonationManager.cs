using UnityEngine;

public class DonationManager : MonoBehaviour
{
    public static DonationManager instance;
    public GameObject canvasDonacion; 

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AceptarDonacion()
    {
        CharacterAttributes personaje = GameManager.instance.personajeActual;

        if (personaje == null)
        {
            Debug.LogWarning("No hay personaje actual asignado en GameManager.");
            return;
        }

        int libroID = personaje.libroDonadoID;
        Debug.Log($"Buscando libro donado con ID: {libroID}");

        if (canvasDonacion != null && !canvasDonacion.activeSelf)
            canvasDonacion.SetActive(true);

        BookData[] libros = Resources.FindObjectsOfTypeAll<BookData>();
        Debug.Log($"Libros encontrados: {libros.Length}");

        foreach (BookData libro in libros)
        {
            if (libro.libroID == libroID)
            {
                libro.gameObject.SetActive(true);
                Debug.Log($"Libro con ID {libroID} activado.");

                if (!string.IsNullOrEmpty(libro.tipoLibro))
                {
                    ShelfManager.instance.SumarLibroEsperadoPorGenero(libro.tipoLibro);
                    Debug.Log($"📚 Sumar libro al género {libro.tipoLibro}");
                }

                break;
            }
        }

        CameraManager.instance.DesctivarPanelDonar();
        GameManager.instance.LibroDonado();
        canvasDonacion.SetActive(false);
    }

}