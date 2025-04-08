using UnityEngine;

public class BookDrag : MonoBehaviour
{
    public Camera dragCamera;
    public LayerMask shelfMask;
    public LayerMask bookMask;

    private Vector3 offset;
    private bool isBeingHeld = false;
    private Vector3 lastValidPosition;

    public float snapGridSize = 1f; // Tamaño de cada celda de la grilla (1x1 por defecto)


    void Start()
    {
        lastValidPosition = transform.position;
    }

    void Update()
    {
        if (isBeingHeld && dragCamera != null)
        {
            Vector3 mousePos = dragCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f; // asegurarse de que no se mueva en Z
            transform.position = mousePos + offset;
        }
    }

    private void OnMouseDown()
    {
        if (dragCamera == null) return;

        Vector3 mousePos = dragCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        offset = transform.position - mousePos;
        isBeingHeld = true;
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;

        bool isInShelf = Physics2D.OverlapPoint(transform.position, shelfMask);

        bool isOverlappingBook = false;
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            transform.position,
            GetComponent<Collider2D>().bounds.size * 0.9f,
            0f,
            bookMask
        );

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject != this.gameObject)
            {
                isOverlappingBook = true;
                break;
            }
        }

        if (isInShelf && !isOverlappingBook)
        {
            lastValidPosition = GetSnappedPosition(transform.position);
            transform.position = lastValidPosition;
        }
        else
        {
            transform.position = lastValidPosition;
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (GetComponent<Collider2D>() != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, GetComponent<Collider2D>().bounds.size * 0.9f);
        }
    }


    Vector3 GetSnappedPosition(Vector3 position)
    {
        float snappedX = Mathf.Round(position.x / snapGridSize) * snapGridSize;
        float snappedY = Mathf.Round(position.y / snapGridSize) * snapGridSize;
        return new Vector3(snappedX, snappedY, 0f);
    }

}
