using UnityEngine;

public class BookData : MonoBehaviour
{
    public string titulo;
    public string descripcion;
    public Sprite imagenLibro;

    private void OnMouseDown()
    {
        BookManager.instance.MostrarInformacion(this);
    }
}
