using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public Camera[] cameras;
    public GameObject[] canvasObjects;
    private int currentCameraIndex = 0;
    private bool verificacionInicialHecha = false;
    public Button botonCambiarCamara1;
    public Button botonCambiarCamara2;
    public Button botonCambiarCamara3;
    public GameObject panelReparacion;
    public GameObject panelPortada;
    public GameObject panelPortada2;
    public BookCoverManager bookCoverManager;
    public GameObject panelHechizo;
    public GameObject panelDonar;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            bool isActive = (i == 0);
            cameras[i].enabled = isActive;
            if (canvasObjects != null && i < canvasObjects.Length)
                canvasObjects[i].SetActive(isActive);
        }
    }

    public void CambiarCamara(int cameraIndex)
    {
        if (cameraIndex < 0 || cameraIndex >= cameras.Length)
        {
            Debug.LogWarning("Índice de cámara fuera de rango.");
            return;
        }

        cameras[currentCameraIndex].enabled = false;
        if (canvasObjects != null && currentCameraIndex < canvasObjects.Length)
            canvasObjects[currentCameraIndex].SetActive(false);

        currentCameraIndex = cameraIndex;

        cameras[currentCameraIndex].enabled = true;
        if (canvasObjects != null && currentCameraIndex < canvasObjects.Length)
            canvasObjects[currentCameraIndex].SetActive(true);

        if (cameraIndex == 1)
        {
            ShelfManager.instance?.IntentarDesorganizarLibros();

            if (!verificacionInicialHecha)
            {
                StartCoroutine(VerificarEstantesDespuesDeFrame());
                verificacionInicialHecha = true;
            }
        }
    }

    private System.Collections.IEnumerator VerificarEstantesDespuesDeFrame()
    {
        yield return null; 

        ShelfEstante[] estantes = FindObjectsOfType<ShelfEstante>();
        foreach (var estante in estantes)
        {
            estante.VerificarEstante();
        }
    }

    public void DesactivarBotonCamara()
    {
        botonCambiarCamara1.interactable = false;
        botonCambiarCamara2.interactable = false;
    }

    public void ActivarBotonCamara()
    {
        botonCambiarCamara1.interactable = true;
        botonCambiarCamara2.interactable = true;
        Debug.Log("botones habilitados");
    }

    public void ActivarPanelReparacion()
    {
        panelReparacion.gameObject.SetActive(true);
        TaskManager.instance.OcultarBotonTareas();
        Debug.Log("PANEL RESTAURACION HABILITADO");
    }

    public void DesactivarPanelReparacion()
    {
        panelReparacion.gameObject.SetActive(false);
    }

    public void ActivarPanelPortada()
    {
        panelPortada.gameObject.SetActive(true);
        TaskManager.instance.OcultarBotonTareas();
        if (bookCoverManager != null)
        {
            bookCoverManager.ActualizarTituloLibro();
        }
    }

    public void DesctivarPanelPortada()
    {
        panelPortada.gameObject.SetActive(false);
        panelPortada2.gameObject.SetActive(false);
    }

    public void ActivarPanelHechizo()
    {
        panelHechizo.gameObject.SetActive(true);
        TaskManager.instance.OcultarBotonTareas();
    }

    public void DesctivarPanelHechizo()
    {
        panelHechizo.gameObject.SetActive(false);
    }

    public void ActivarPanelDonar()
    {
        DonationManager.instance.ActualizarPortada();
        panelDonar.gameObject.SetActive(true);
        TaskManager.instance.OcultarBotonTareas();
    }

    public void DesctivarPanelDonar()
    {
        panelDonar.gameObject.SetActive(false);
    }

    public void ActivarCamaraPrincipal()
    {
        CambiarCamara(0);
    }
}