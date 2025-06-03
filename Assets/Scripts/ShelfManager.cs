using System.Collections.Generic;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{
    public static ShelfManager instance;
    public AudioSource audioLibroCorrecto;
    private bool librosDesorganizados = false; 

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

    public void DesorganizarLibros()
    {
        List<Transform> librosActivos = new List<Transform>();
        ShelfEstante[] estantes = FindObjectsOfType<ShelfEstante>();
        Debug.Log($"Cantidad de estantes encontrados: {estantes.Length}");
        List<Transform> slotsDisponibles = new List<Transform>();

        foreach (ShelfEstante estante in estantes)
        {
            foreach (Transform slot in estante.transform)
            {
                if (slot.childCount > 0)
                {
                    Transform libro = slot.GetChild(0);
                    if (libro.gameObject.activeSelf)
                    {
                        librosActivos.Add(libro);
                        libro.SetParent(null);
                    }
                }
                slotsDisponibles.Add(slot);
            }
        }

        Shuffle(slotsDisponibles);

        for (int i = 0; i < librosActivos.Count && i < slotsDisponibles.Count; i++)
        {
            Transform libro = librosActivos[i];
            Transform nuevoSlot = slotsDisponibles[i];

            libro.SetParent(nuevoSlot);
            libro.localPosition = Vector3.zero;
        }

        foreach (ShelfEstante estante in estantes)
        {
            estante.VerificarEstante();
        }
        Debug.Log($"Libros reorganizados: {librosActivos.Count}");
    }

    public void IntentarDesorganizarLibros()
    {
        if (!librosDesorganizados)
        {
            DesorganizarLibros();
            librosDesorganizados = true;
        }
    }

    public void ReiniciarEstado()
    {
        librosDesorganizados = false;
    }
    private void Shuffle<T>(List<T> lista)
    {
        for (int i = 0; i < lista.Count; i++)
        {
            int randomIndex = Random.Range(i, lista.Count);
            T temp = lista[i];
            lista[i] = lista[randomIndex];
            lista[randomIndex] = temp;
        }
    }
}
