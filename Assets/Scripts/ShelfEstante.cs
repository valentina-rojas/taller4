using UnityEngine;
using TMPro;

public class ShelfEstante : MonoBehaviour
{
    public string genero;
    public int cantidadLibrosEsperados; 
    public TMP_Text cartelGenero;

    private Color colorOriginal;

    private void Start()
    {
        if (cartelGenero != null)
        {
            colorOriginal = cartelGenero.color;
            if (colorOriginal.a < 0.1f) 
            {
                colorOriginal.a = 1f; 
                cartelGenero.color = colorOriginal; 
            }
        }
    }


    public void VerificarEstante()
    {
        int librosCorrectos = 0;
        bool hayLibroIncorrecto = false;

        foreach (Transform slot in transform)
        {
            if (slot.childCount == 1)
            {
                Transform libro = slot.GetChild(0);
                BookData data = libro.GetComponent<BookData>();

                if (data != null)
                {
                    if (data.tipoLibro == genero)
                    {
                        librosCorrectos++;
                    }
                    else
                    {
                        hayLibroIncorrecto = true;
                    }
                }
            }
        }

        if (librosCorrectos == cantidadLibrosEsperados && !hayLibroIncorrecto)
        {
            cartelGenero.color = new Color(1f, 0.85f, 0f); 
        }
        else
        {
            cartelGenero.color = colorOriginal;
        }
    }

}

