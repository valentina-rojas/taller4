using UnityEngine;
using   UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

Transform parentAfterDrag;

public void OnBeginDrag(PointerEventData eventData) {

  Debug.Log("lll");
  parentAfterDrag = transform.parent;
  transform.SetParent(transform.root);
  transform.SetAsLastSibling();
}

public void OnDrag(PointerEventData eventData) {
     Debug.Log("222");
   transform.position = Input.mousePosition;
}

public void OnEndDrag(PointerEventData eventData) {
  Debug.Log("333");
  transform.SetParent(parentAfterDrag);
}


}
