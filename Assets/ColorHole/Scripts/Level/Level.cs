using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<LevelItem> _levelItems;
    [SerializeField] private int _totalObjectCount;
    [SerializeField] private int _obstaclesCount;
    [SerializeField] private bool _isMove;
    public int TotalObjectCount { get => _totalObjectCount; set => _totalObjectCount = value; }
    public int ObstacleCount { get => _obstaclesCount; set => _obstaclesCount = value; }
    public bool IsMove { get => _isMove; set => _isMove = value; }
    private bool _isLastPart;

    private void Update()
    {
        if (IsMove)
        {
            _isLastPart = true;
            Game.isMoving = false;
            HoleMovement.Instance.Move();
        }
    }
    public void Active()
    {
        gameObject.SetActive(true);
        TotalObjectCount = GameObject.FindGameObjectsWithTag("Object").Length;
        ObstacleCount = GameObject.FindGameObjectsWithTag("Obstacle").Length;
        LevelItems();
    }
    public void DecreateObjectCount()
    {
        TotalObjectCount--;
        LevelItem().DecreateObjectCount();
        CheckIsMove();

        if (TotalObjectCount <= 0)
            GameManager.Instance.OnLevelCompleted();
    }
    private LevelItem LevelItem()
    {
        return _levelItems.Where(x => x.IsOk == false).FirstOrDefault();
    }
    private void CheckIsMove()
    {
        if (_isLastPart) return;
        IsMove = _levelItems.Any(x => x.IsOk == true);
    }
    private void LevelItems()
    {
        foreach (var part in _levelItems)
        {
            part.CheckObjectCount();
        }
    }
    public void ChangeStateRigidbody(bool val)
    {
        foreach (var obstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            obstacle.GetComponent<Rigidbody>().useGravity = val;
        }
    }
}
