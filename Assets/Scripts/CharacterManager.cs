using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
    private bool yaAtendido = false;

    public CharacterAttributes UltimoPersonajeAtendido { get; private set; }

    // Nueva lista para guardar todos los personajes atendidos en el día
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

        // Agregar el personaje a la lista si no está ya
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
        }

        yaAtendido = true;
    }

    public void ResetearAtencion()
    {
        yaAtendido = false;
        UltimoPersonajeAtendido = null;
    }

    // Nuevo método para obtener la lista completa de personajes atendidos
    public List<CharacterAttributes> GetPersonajesAtendidos()
    {
        return personajesAtendidos;
    }

    // Opcional: Método para resetear el historial (ej: al final del día)
    public void ResetearHistorial()
    {
        personajesAtendidos.Clear();
    }
}
