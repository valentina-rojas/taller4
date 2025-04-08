using UnityEngine;

public class ShelfManager : MonoBehaviour
{
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
}
