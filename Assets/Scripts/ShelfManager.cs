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

    public bool RevisarOrganizacion()
    {
        ShelfSlots[] estantesEnEscena = FindObjectsOfType<ShelfSlots>();

        foreach (ShelfSlots estante in estantesEnEscena)
        {
            foreach (Transform libro in estante.transform)
            {
                BookData book = libro.GetComponent<BookData>();
                if (book != null && book.tipoLibro != estante.generoPermitido)
                {
                    Debug.Log("Libro incorrecto en estante de " + estante.generoPermitido);
                    return false;
                }
            } 
        }

        Debug.Log("Todo bien organizado.");
        TaskManager.instance.CompletarTareaPorID(1);
        return true;
    }


}
