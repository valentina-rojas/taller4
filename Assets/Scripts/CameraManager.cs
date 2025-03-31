using UnityEngine;

public class CameraManager : MonoBehaviour
{
  public Camera[] cameras; // Array de cámaras
    private int currentCameraIndex = 0; // Índice de la cámara activa

    void Start()
    {
        // Desactivar todas las cámaras excepto la primera
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].enabled = (i == 0);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) // Cambia de cámara con "C"
        {
            ChangeCamera();
        }
    }

    void ChangeCamera()
    {
        // Desactivar la cámara actual
        cameras[currentCameraIndex].enabled = false;

        // Mover al siguiente índice de cámara (cíclico)
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // Activar la nueva cámara
        cameras[currentCameraIndex].enabled = true;
    }
}
