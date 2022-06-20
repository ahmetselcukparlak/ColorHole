using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
        {
            Magnet.Instance.RemoveFromMagnetField(other.attachedRigidbody);

            GameManager.Instance.Level.DecreateObjectCount();
            Destroy(other.gameObject);
            Vibrator.Vibrate(40);
        }
        if (other.CompareTag("NullObject"))
        {
            Magnet.Instance.RemoveFromMagnetField(other.attachedRigidbody);
            Destroy(other.gameObject);
            Vibrator.Vibrate(40);
        }
        if (other.CompareTag("Obstacle"))
        {
            Magnet.Instance.RemoveFromMagnetField(other.attachedRigidbody);
            Invoke("OnLevelFailed", 0.5f);
            Destroy(other.gameObject);
            Vibrator.Vibrate(50);
            Game.isGameOver = true;
            GameManager.Instance.GameCamera.GetComponent<CameraShake>().ShakeIt();
        }
    }

    private void OnLevelFailed()
    {
        GameManager.Instance.OnLevelFailed();
    }

}
