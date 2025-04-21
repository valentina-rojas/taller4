using UnityEngine;

public class BookCoverManager : MonoBehaviour
{
    public GameObject portadaEditable;  // Panel editable
    public GameObject portadaFinal;     // Panel de preview final
 public RectTransform areaPortada;   // El área válida de la portada
 

    public void Finalizar()
    {
        portadaFinal.SetActive(true);

        foreach (Transform child in portadaEditable.transform)
        {
            RectTransform childRect = child.GetComponent<RectTransform>();

            if (RectTransformUtility.RectangleContainsScreenPoint(areaPortada, child.position, null))
            {
                // Solo clona los objetos que están dentro del área de la portada
                GameObject copia = Instantiate(child.gameObject, portadaFinal.transform);

                // Desactiva el componente DraggableItem para que ya no se puedan mover
                DraggableItem draggable = copia.GetComponent<DraggableItem>();
                if (draggable != null)
                {
                    Destroy(draggable);
                }
            }
        }

        portadaEditable.SetActive(false);
    }
}