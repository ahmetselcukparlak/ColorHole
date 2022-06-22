using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleMovement : Singleton<HoleMovement>
{
    [SerializeField] private float _timer;
    public float Timer { get => _timer; set => _timer = value; }

    [SerializeField] private GameObject _door;

    private float elapsedTime;
    [SerializeField] private AnimationCurve curve;


    [SerializeField] private List<MoveLimits> moveLimits;
    private MoveLimits _moveLimit;
    public MoveLimits MoveLimit { get => _moveLimit; set => _moveLimit = value; }


    private void Start()
    {
        MoveLimit = moveLimits[0];
    }
    public void Move()
    {
        elapsedTime += Time.deltaTime;

        if (transform.position.x > 0.01f || transform.position.x < -0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, transform.position.y, transform.position.z), curve.Evaluate(elapsedTime / (Timer / 2)));
            GameManager.Instance.Level.ChangeStateRigidbody(false);
            Vector3 pos = _door.transform.position;
            pos.y = -0.5f;
            _door.transform.position = Vector3.Lerp(_door.transform.position, pos, curve.Evaluate(elapsedTime / (Timer / 2)));
        }
        else
        {
            if (transform.position.z < 9.49)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(0, transform.position.y, 9.5f), curve.Evaluate(elapsedTime / (Timer * 5)));
                var camera = GameManager.Instance.GameCamera;
                camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(0, camera.transform.position.y, 9.5f), curve.Evaluate(elapsedTime / (Timer * 5)));
                Magnet.Instance.MagnetForce = 1000000;
            }
            else
            {
                Magnet.Instance.ResetMagnetForce();
                MoveLimit = moveLimits[1];
                GameManager.Instance.Level.IsMove = false;
                GameManager.Instance.Level.ChangeStateRigidbody(true);
            }
        }
    }

}
