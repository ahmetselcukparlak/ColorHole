using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private Material _ground;
    [SerializeField] private SpriteRenderer _groundBorder;
    [SerializeField] private Material _obstacle;
    [SerializeField] private MeshRenderer _hole;

    [SerializeField] private List<Colors> Colors;
    [SerializeField] private Colors _colors;
    public Colors Color { get => _colors; set => _colors = value; }

    public void SetColors()
    {
        //Color = Colors[Random.Range(0, Colors.Count)];
        var gameManager = GameManager.Instance;
        Color = Colors[(gameManager.CurrentLevel - 1) % 3];

        gameManager.GameCamera.backgroundColor = Color.BackgroundColor;
        _hole.material = Color.Hole;
        _groundBorder.color = Color.GroundBorderColor;
        _obstacle.color = Color.ObstacleColor;
        _ground.color = Color.GroundColor;
    }
}