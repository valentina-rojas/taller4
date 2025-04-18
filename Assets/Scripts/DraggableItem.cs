using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
  [HideInInspector] public Transform parentAfterDrag;
  private Canvas canvas;
  public Image image;


  private void Awake()
  {
    // Buscamos el canvas más cercano
    canvas = GetComponentInParent<Canvas>();
  }

  public void OnBeginDrag(PointerEventData eventData)
  {
    // Debug.Log("begin drag");

    parentAfterDrag = transform.parent;
    transform.SetParent(canvas.transform);
    transform.SetAsLastSibling();

    image.raycastTarget = false;
  }

  public void OnDrag(PointerEventData eventData)
  {
    Vector2 localPoint;
    RectTransformUtility.ScreenPointToLocalPointInRectangle(
        canvas.transform as RectTransform,
        eventData.position,
        canvas.worldCamera,
        out localPoint
    );

    transform.localPosition = localPoint;
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    //  Debug.Log("end drag");
    transform.SetParent(parentAfterDrag);
    image.raycastTarget = true;
  }
}
