using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharacterAttributes))]
public class CharacterAttributesEditor : Editor
{
    SerializedProperty tipoDePedido;
    SerializedProperty dialogueLinesInicio;
    SerializedProperty dialogueLinesBuena;
    SerializedProperty dialogueLinesMala;
    SerializedProperty spriteRespuestaBuena;
    SerializedProperty spriteRespuestaMala;

    SerializedProperty libroDeseadoID;
    SerializedProperty tipoPreferido;
    SerializedProperty tituloLibroPrestado;

    SerializedProperty stickersRequeridos;
    SerializedProperty tituloLibroPortada;

    SerializedProperty hechizoSolicitado;
    SerializedProperty libroDonadoID;

    SerializedProperty nombreDelCliente;
    SerializedProperty descripcionPedido;

    void OnEnable()
    {
        tipoDePedido = serializedObject.FindProperty("tipoDePedido");
        dialogueLinesInicio = serializedObject.FindProperty("dialogueLinesInicio");
        dialogueLinesBuena = serializedObject.FindProperty("dialogueLinesBuena");
        dialogueLinesMala = serializedObject.FindProperty("dialogueLinesMala");
        spriteRespuestaBuena = serializedObject.FindProperty("spriteRespuestaBuena");
        spriteRespuestaMala = serializedObject.FindProperty("spriteRespuestaMala");

        libroDeseadoID = serializedObject.FindProperty("libroDeseadoID");
        tipoPreferido = serializedObject.FindProperty("tipoPreferido");
        tituloLibroPrestado = serializedObject.FindProperty("tituloLibroPrestado");

        stickersRequeridos = serializedObject.FindProperty("stickersRequeridos");
        tituloLibroPortada = serializedObject.FindProperty("tituloLibroPortada");

        hechizoSolicitado = serializedObject.FindProperty("hechizoSolicitado");
        libroDonadoID = serializedObject.FindProperty("libroDonadoID");

        nombreDelCliente = serializedObject.FindProperty("nombreDelCliente");
        descripcionPedido = serializedObject.FindProperty("descripcionPedido");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(tipoDePedido);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("📖 Diálogos", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(dialogueLinesInicio);
        EditorGUILayout.PropertyField(dialogueLinesBuena);
        EditorGUILayout.PropertyField(dialogueLinesMala);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("🎭 Sprites de Respuesta", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(spriteRespuestaBuena);
        EditorGUILayout.PropertyField(spriteRespuestaMala);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("📚 Datos del Cliente", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(nombreDelCliente);
        EditorGUILayout.PropertyField(descripcionPedido);

        CharacterAttributes.TipoDePedido tipo = (CharacterAttributes.TipoDePedido)tipoDePedido.enumValueIndex;

        EditorGUILayout.Space();
        switch (tipo)
        {
            case CharacterAttributes.TipoDePedido.BuscarLibro:
                EditorGUILayout.LabelField("🔍 Preferencias del Libro", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(libroDeseadoID);
                EditorGUILayout.PropertyField(tipoPreferido);
                EditorGUILayout.PropertyField(tituloLibroPrestado);
                break;

            case CharacterAttributes.TipoDePedido.HacerPortada:
                EditorGUILayout.LabelField("🎨 Portada", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(stickersRequeridos);
                EditorGUILayout.PropertyField(tituloLibroPortada);
                break;

            case CharacterAttributes.TipoDePedido.HechizarLibro:
                EditorGUILayout.LabelField("✨ Hechizo", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(hechizoSolicitado);
                break;

            case CharacterAttributes.TipoDePedido.DonarLibro:
                EditorGUILayout.LabelField("📤 Donación", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(libroDonadoID);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}