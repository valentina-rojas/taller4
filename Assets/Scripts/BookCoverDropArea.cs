using UnityEngine;
using UnityEngine.EventSystems;

public class BookCoverDropArea : MonoBehaviour, IDropHandler
{
    public BookCoverManager coverManager; // Asigna esto en el Inspector

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null)
        {
            droppedObject.transform.SetParent(this.transform);
            coverManager.VerificarElementosEnPortada(); // Verifica después del drop
        }
    }
}
