using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BookCoverManager : MonoBehaviour
{
    public GameObject portadaEditable;
    public GameObject portadaFinal;
    public Button finalizarButton;
    public RectTransform areaPortada;

    private void Start()
    {
        finalizarButton.interactable = false;
    }

    public void VerificarElementosEnPortada()
    {
        finalizarButton.interactable = portadaEditable.transform.childCount > 0;
    }

public void Finalizar()
{
    portadaFinal.SetActive(true);

    List<StickerID> stickersUsados = new List<StickerID>();

    foreach (Transform child in portadaEditable.transform)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(areaPortada, RectTransformUtility.WorldToScreenPoint(null, child.position)))
        {
            StickerData data = child.GetComponent<StickerData>();
            if (data != null && !stickersUsados.Contains(data.stickerID))
            {
                stickersUsados.Add(data.stickerID);
            }

            DraggableItem draggable = child.GetComponent<DraggableItem>();
            if (draggable != null)
                draggable.enabled = false;
        }
        else
        {
            child.gameObject.SetActive(false);
        }
    }

    GameManager.instance.CompletarPortada(stickersUsados);
}
}