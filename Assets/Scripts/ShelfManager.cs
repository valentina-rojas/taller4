using UnityEngine;

public class ShelfManager : MonoBehaviour
{

    public ShelfSlots[] estantes;


    public bool EstaDisponible(RectTransform libroRect)
    {
        foreach (Transform hijo in transform)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(hijo.GetComponent<RectTransform>(), libroRect.position))
            {
                return false; // Ya hay un libro en esa zona
            }
        }
        return true;
    }

    public bool RevisarOrganizacion()
    {
        foreach (ShelfSlots estante in estantes)
        {
            foreach (Transform libro in estante.transform)
            {
                BookData book = libro.GetComponent<BookData>();
                if (book != null && book.tipoLibro != estante.generoPermitido)
                {
                    Debug.Log("Error en el estante de " + estante.generoPermitido);
                    return false;
                }
            }
        }
        Debug.Log("Todo bien organizado.");
        return true;
    }
}
