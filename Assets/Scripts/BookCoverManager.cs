using UnityEngine;

public class BookCoverManager : MonoBehaviour
{
    public GameObject portadaEditable;
    public GameObject portadaFinal;



    public void Finalizar()
    {
        portadaFinal.SetActive(true);

       foreach (Transform child in portadaEditable.transform)
{
    RectTransform childRect = child.GetComponent<RectTransform>();

    DraggableItem draggable = child.GetComponent<DraggableItem>();
    if (draggable != null)
    {
        draggable.enabled = false;
    }
}


     //   portadaEditable.SetActive(false);


    }


}