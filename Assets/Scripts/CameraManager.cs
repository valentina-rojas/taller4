using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera[] cameras;
    public GameObject[] canvasObjects;

    private int currentCameraIndex = 0;

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

    void Update()
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
    }
}
