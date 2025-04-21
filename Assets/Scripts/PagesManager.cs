using UnityEngine;
using System.Collections.Generic;

public class PagesManager : MonoBehaviour
{
    public static PagesManager instance;

  public PagesSlot[] slots;

    private void Awake()
    {
        instance = this;
    }

    public void CheckOrder()
    {
        foreach (PagesSlot slot in slots)
        {
            if (slot.transform.childCount == 0)
            {
                Debug.Log("Faltan páginas.");
                return;
            }

            PageData pageData = slot.transform.GetChild(0).GetComponent<PageData>();
            if (pageData.pageID != slot.expectedPageID)
            {
                Debug.Log("Página fuera de lugar.");
                return;
            }
        }

        Debug.Log("¡Libro restaurado correctamente!");

    }

}
