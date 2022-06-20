using UnityEngine;

[System.Serializable]
public class Colors
{
    [SerializeField] private Color _backgroundColor;
    [SerializeField] private Color _groundColor;
    [SerializeField] private Color _groundBorderColor;
    [SerializeField] private Color _obstacleColor;
    [SerializeField] private Material _hole;

    public Color BackgroundColor { get => _backgroundColor; }
    public Color GroundColor { get => _groundColor; }
    public Color ObstacleColor { get => _obstacleColor; }
    public Color GroundBorderColor { get => _groundBorderColor; }
    public Material Hole { get => _hole; set => _hole = value; }
}