using UnityEngine;
using UnityEngine.UI;

public class BookCoverManager : MonoBehaviour
{
    public GameObject portadaEditable;
    public GameObject portadaFinal;
    public Button finalizarButton;

    private void Start()
    {
        finalizarButton.interactable = false; 
    }

    public void VerificarElementosEnPortada()
    {
        if (portadaEditable.transform.childCount > 0)
        {
            finalizarButton.interactable = true;
        }
    }

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