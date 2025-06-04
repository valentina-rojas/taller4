using UnityEngine;
using System.Collections.Generic;

public class CharacterAttributes : MonoBehaviour
{
    public enum TipoDePedido
    {
        BuscarLibro,
        RepararLibro,
        HacerPortada,
        HechizarLibro,
        JuegoTrivia
    }

    public enum Hechizo
    {
        Ninguno,      
        Sellado,
        Proteccion,
        Traduccion,
        Restauracion,
        Comunicacion
    }

    [Header("Tipo de pedido de este personaje")]
    public TipoDePedido tipoDePedido;

    [Header("Diálogos iniciales")]
    [SerializeField, TextArea(2, 4)] private string[] dialogueLinesInicio;

    [Header("Diálogos si la recomendación fue buena")]
    [SerializeField, TextArea(2, 4)] private string[] dialogueLinesBuena;

    [Header("Diálogos si la recomendación fue mala")]
    [SerializeField, TextArea(2, 4)] private string[] dialogueLinesMala;

    [Header("Sprites para respuestas")]
    public Sprite spriteRespuestaBuena;
    public Sprite spriteRespuestaMala;

    [Header("Preferencias del personaje")]
    public int libroDeseadoID;
    public string tipoPreferido;

    [Header("Datos para el historial")]
    public string nombreDelCliente;      
    [TextArea(1, 3)]
    public string descripcionPedido;     
        
    [Header("Libro prestado (opcional)")]
    public string tituloLibroPrestado = ""; 
    [Header("Stickers requeridos (opcional)")]
    public List<StickerID> stickersRequeridos = new List<StickerID>();
    [Header("Título del libro para portada (opcional)")]
    public string tituloLibroPortada = "";

    [Header("Hechizo solicitado (opcional)")]
    public Hechizo hechizoSolicitado = Hechizo.Ninguno;

    public string[] GetDialogueInicio() => dialogueLinesInicio;
    public string[] GetDialogueBuena() => dialogueLinesBuena;
    public string[] GetDialogueMala() => dialogueLinesMala;
}

