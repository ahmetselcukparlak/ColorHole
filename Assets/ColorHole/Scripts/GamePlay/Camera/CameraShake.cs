using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 cameraInitialPosition;
    public float shakeMagnetude = 0.05f, shakeTime = 0.5f;
    public float value;
    public Camera mainCamera;

    public void ShakeIt()
    {
        cameraInitialPosition = mainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", shakeTime);
    }

    void StartCameraShaking()
    {
        float cameraShakingOffsetX = Random.value * shakeMagnetude * 2 - shakeMagnetude;
        //float cameraShakingOffsetY = Random.value * shakeMagnetude * 2 - shakeMagnetude;
        Vector3 cameraIntermadiatePosition = mainCamera.transform.position;
        cameraIntermadiatePosition.x += cameraShakingOffsetX;
        //cameraIntermadiatePosition.y += cameraShakingOffsetY;
        //mainCamera.transform.position = cameraIntermadiatePosition;
        mainCamera.transform.position = new Vector3(
            Mathf.Clamp(cameraIntermadiatePosition.x, -value, value),
            cameraIntermadiatePosition.y,
            cameraIntermadiatePosition.z
        );
    }

    void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        mainCamera.transform.position = cameraInitialPosition;
    }
}
