using System.Collections.Generic;
using UnityEngine;

public class CobwebManager : MonoBehaviour
{
    public static CobwebManager instance;

    private List<CobwebCleaning> telarañasActivas = new List<CobwebCleaning>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
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
        // Acá ponés lo que quieras que pase
        Debug.Log("Podés avanzar al siguiente paso ✨");
        // Por ejemplo: activar un objeto, abrir una puerta, cambiar de escena, etc.
    }
}
