using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnChangePosition : MonoBehaviour
{
    public PolygonCollider2D hole2DCollider;
    public PolygonCollider2D ground2DCollider;
    public MeshCollider GeneratedMeshCollider;
    public float initialScale = 0.5f;
    private Mesh GeneratedMesh;


    private float x;
    private float y;
    [SerializeField] private float _moveSpeed;


    private Vector3 touch;
    private Vector3 targetPos;

    private void FixedUpdate()
    {
        if (transform.hasChanged)
        {
            transform.hasChanged = false;
            hole2DCollider.transform.position = new Vector2(transform.position.x, transform.position.z);
            hole2DCollider.transform.localScale = transform.localScale * initialScale;
            MakeHole2D();
            Make3DMeshCollider();
        }
    }
    private void Update()
    {
#if UNITY_EDITOR
        if (!GameManager.Instance.Level.IsMove)
        {
            Game.isMoving = Input.GetMouseButton(0);
            if (!Game.isGameOver && Game.isMoving)
            {
                MoveHole();
            }
        }
#else
        if (!GameManager.Instance.Level.IsMove)
        {
            Game.isMoving = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved;
            if (!Game.isGameOver && Game.isMoving)
            {
                MoveHole();
            }
        }
#endif

    }

    private void MoveHole()
    {
        UIManager.Instance.tutorialHint.SetActive(false);
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        touch = Vector3.Lerp(transform.position, transform.position + new Vector3(x, 0f, y), _moveSpeed * Time.deltaTime);

        var moveLimit = HoleMovement.Instance.MoveLimit;
        targetPos = new Vector3(
            Mathf.Clamp(touch.x, moveLimit.StartPos.x, moveLimit.EndPos.x),
            touch.y,
            Mathf.Clamp(touch.z, moveLimit.StartPos.y, moveLimit.EndPos.y)
        );

        transform.position = targetPos;
    }

    private void MakeHole2D()
    {
        Vector2[] PointPositions = hole2DCollider.GetPath(0);
        for (var i = 0; i < PointPositions.Length; i++)
        {
            PointPositions[i] = hole2DCollider.transform.TransformPoint(PointPositions[i]);
        }
        ground2DCollider.pathCount = 2;
        ground2DCollider.SetPath(1, PointPositions);
    }

    private void Make3DMeshCollider()
    {
        if (GeneratedMesh != null) Destroy(GeneratedMesh);
        GeneratedMesh = ground2DCollider.CreateMesh(true, true);
        GeneratedMeshCollider.sharedMesh = GeneratedMesh;
    }
}