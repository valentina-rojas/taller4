using System.Collections.Generic;
using UnityEngine;

public class CobwebManager : MonoBehaviour
{
    public static CobwebManager instance;

    private GameManager gameManager;

    private List<CobwebCleaning> telarañasActivas = new List<CobwebCleaning>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();


        if (gameManager == null)
            Debug.LogError("GameManager no encontrado en la escena.");


    }



    public void RegistrarTelaraña(CobwebCleaning telaraña)
    {
        telarañasActivas.Add(telaraña);
    }

    public void EliminarTelaraña(CobwebCleaning telaraña)
    {
        telarañasActivas.Remove(telaraña);

        if (telarañasActivas.Count == 0)
        {
            Debug.Log("¡Todas las telarañas fueron limpiadas!");
            AccionFinal();
        }
    }

    private void AccionFinal()
    {
        Debug.Log("Podés avanzar al siguiente paso ✨");

       // gameManager.IniciarSpawnDePersonajes();
      TaskManager.instance.CompletarTareaPorID(0);

    }
}
