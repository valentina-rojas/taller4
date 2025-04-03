using UnityEngine;

public class CameraManager : MonoBehaviour
{
  public Camera[] cameras; 
    private int currentCameraIndex = 0; 

    void Start()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].enabled = (i == 0);
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
       
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        cameras[currentCameraIndex].enabled = true;
    }
}
