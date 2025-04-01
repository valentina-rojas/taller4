using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class BookPicking : MonoBehaviour
{
    public string titulo;
    public string descripcion;
    public Sprite imagenLibro; // Imagen del libro para mostrar en la UI

    public GameObject panelInfoLibro; // Panel que se activa al hacer clic
    public TMP_Text tituloTexto;
    public TMP_Text descripcionTexto;
    public Image imagenLibroUI;
    
    private void OnMouseDown()
    {
        MostrarInformacion();
    }

    public void MostrarInformacion()
    {
        panelInfoLibro.SetActive(true);
        tituloTexto.text = titulo;
        descripcionTexto.text = descripcion;
       imagenLibroUI.sprite = imagenLibro;
        
        // Guardar referencia del libro seleccionado
      //  GameManager.instance.LibroSeleccionado = this;
    }

    public void ConfirmarSeleccion()
    {
       // GameManager.instance.ConfirmarLibro();
        panelInfoLibro.SetActive(false);
    }

    public void CancelarSeleccion()
    {
        panelInfoLibro.SetActive(false);
    }
}
