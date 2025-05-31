using System.Collections.Generic;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{
    public static ShelfManager instance;
    public AudioSource audioLibroCorrecto;

    private void Awake()
    {
        instance = this;
    }

    public void RevisarOrganizacionConDelay()
    {
        Invoke(nameof(RevisarOrganizacion), 0.1f);
    }

    public void RevisarOrganizacion()
    {
        ShelfEstante[] estantes = FindObjectsOfType<ShelfEstante>();
        bool todosCorrectos = true;

        foreach (ShelfEstante estante in estantes)
        {
            int librosCorrectos = 0;

            foreach (Transform slot in estante.transform)
            {
                if (slot.childCount == 1)
                {
                    Transform libro = slot.GetChild(0);
                    BookData book = libro.GetComponent<BookData>();

                    if (book != null && book.tipoLibro == estante.genero)
                    {
                        librosCorrectos++;
                    }
                    else if (book != null)
                    {
                        Debug.Log($"Libro '{book.titulo}' mal ubicado en estante '{estante.genero}'");
                    }
                }
            }

            if (librosCorrectos != estante.cantidadLibrosEsperados)
            {
                todosCorrectos = false;
            }

            estante.VerificarEstante(); 
        }

        if (todosCorrectos)
        {
            Debug.Log("Todos los libros están correctamente organizados.");
            TaskManager.instance.CompletarTareaPorID(1);
        }
    }

}
