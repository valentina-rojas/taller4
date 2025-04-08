using UnityEngine;

public class BookData : MonoBehaviour
{
    public int libroID;
    public string tipoLibro;
    public string titulo;
    public string descripcion;
    public Sprite imagenLibro;


    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

   /* private void OnMouseDown()
    {
        BookManager.instance.MostrarInformacion(this);
    }*/

    private void OnMouseEnter()
    {
        spriteRenderer.color = new Color(originalColor.r * 0.7f, originalColor.g * 0.7f, originalColor.b * 0.7f);
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = originalColor;
    }
}