using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;


public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public Camera[] cameras;
    public GameObject[] canvasObjects;

    private int currentCameraIndex = 0;

    public Button botonCambiarCamara1;


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
    }


    public void DesactivarBotonCamara()
    {

        botonCambiarCamara1.interactable = false;
    }

    public void ActivarBotonCamara()
    {

        botonCambiarCamara1.interactable = true;
    }


    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeCamera();
        }
    }

    void ChangeCamera()
    {
        cameras[currentCameraIndex].enabled = false;
        if (canvasObjects != null && currentCameraIndex < canvasObjects.Length)
            canvasObjects[currentCameraIndex].SetActive(false);

        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;


        cameras[currentCameraIndex].enabled = true;
        if (canvasObjects != null && currentCameraIndex < canvasObjects.Length)
            canvasObjects[currentCameraIndex].SetActive(true);
    }*/
}
