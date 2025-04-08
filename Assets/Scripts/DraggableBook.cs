using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableBook : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector3 originalPosition;
    private Transform originalParent;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        originalPosition = transform.position;
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(transform.root); // para arrastrar sobre todo el canvas
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / transform.root.GetComponent<Canvas>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        ShelfManager[] zonas = FindObjectsOfType<ShelfManager>();

        foreach (var zona in zonas)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(zona.GetComponent<RectTransform>(), Input.mousePosition))
            {
                if (zona.EstaDisponible(rectTransform))
                {
                    transform.SetParent(zona.transform);
                    return;
                }
            }
        }

        // Si no cayó en zona válida, vuelve a su lugar original
        transform.position = originalPosition;
        transform.SetParent(originalParent);
    }
}
