using UnityEngine;

[System.Serializable]
public class MoveLimits
{
    [SerializeField] private Vector2 _startPos;
    [SerializeField] private Vector2 _endPos;

    public Vector2 StartPos { get => _startPos; set => _startPos = value; }
    public Vector2 EndPos { get => _endPos; set => _endPos = value; }
}