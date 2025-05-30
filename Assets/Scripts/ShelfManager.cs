using UnityEngine;
using System.Collections.Generic;

public class ShelfManager : MonoBehaviour
{
    public static ShelfManager instance;

    public List<ShelfSlots> estantes = new List<ShelfSlots>();

    private void Awake()
    {
        instance = this;
    }

    public bool EstaDisponible(RectTransform libroRect)
    {
        foreach (Transform hijo in transform)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(hijo.GetComponent<RectTransform>(), libroRect.position))
            {
                return false;
            }
        }
        return true;
    }
    public void RevisarOrganizacionConDelay()
    {
        Invoke(nameof(RevisarOrganizacion), 0.1f);  
    }
    public void RevisarOrganizacion()
    {
        ShelfSlots[] estantesEnEscena = FindObjectsOfType<ShelfSlots>();
        bool todosCorrectos = true;

        foreach (ShelfSlots estante in estantesEnEscena)
        {
            foreach (Transform libro in estante.transform)
            {
                if (!libro.gameObject.activeInHierarchy)
                    continue;

                BookData book = libro.GetComponent<BookData>();
                if (book == null)
                    continue;

                if (book.tipoLibro != estante.generoPermitido)
                {
                    Debug.Log($"Libro activo '{book.titulo}' está mal ubicado en '{estante.generoPermitido}'");
                    todosCorrectos = false;
                }
                else
                {
                    Debug.Log($"Libro activo '{book.titulo}' correctamente ubicado en '{estante.generoPermitido}'");
                }
            }
        }

        if (todosCorrectos)
        {
            Debug.Log("Todos los libros activos están correctamente organizados.");
            TaskManager.instance.CompletarTareaPorID(1);
        }
    }
}