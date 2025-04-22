using UnityEngine;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{

     public static CharacterManager instance;
    private bool yaAtendido = false;

      private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

  
    }

    public void AtenderPersonaje(CharacterAttributes personaje)
    {
        if (yaAtendido) return; 

        switch (personaje.tipoDePedido)
        {
            case CharacterAttributes.TipoDePedido.BuscarLibro:
                Debug.Log("Este personaje busca un libro.");
                CameraManager.instance.ActivarBotonCamara();
                BookManager.instance.HabilitarBotonConfirmacion();
                break;

            case CharacterAttributes.TipoDePedido.RepararLibro:
                Debug.Log("Este personaje necesita que repares un libro.");
                CameraManager.instance.ActivarPanelReparacion();
                break;

            case CharacterAttributes.TipoDePedido.HacerPortada:
                Debug.Log("Este personaje quiere que le hagas una portada.");
                CameraManager.instance.ActivarPanelPortada();
                break;
        }

        yaAtendido = true; 
    }

    public void ResetearAtencion()
    {
        yaAtendido = false; 
    }
}
