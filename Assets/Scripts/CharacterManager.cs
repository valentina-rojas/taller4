using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
    private bool yaAtendido = false;

    public CharacterAttributes UltimoPersonajeAtendido { get; private set; }

    private List<CharacterAttributes> personajesAtendidos = new List<CharacterAttributes>();

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

        UltimoPersonajeAtendido = personaje;

        if (!personajesAtendidos.Contains(personaje))
        {
            personajesAtendidos.Add(personaje);
        }

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
            case CharacterAttributes.TipoDePedido.HechizarLibro:
                Debug.Log("Este personaje quiere que le hechices un libro.");
                CameraManager.instance.ActivarPanelHechizo(); 
                break;

        }

        yaAtendido = true;
    }

    public void ResetearAtencion()
    {
        yaAtendido = false;
        UltimoPersonajeAtendido = null;
    }

    public List<CharacterAttributes> GetPersonajesAtendidos()
    {
        return personajesAtendidos;
    }

    public void ResetearHistorial()
    {
        personajesAtendidos.Clear();
    }
}