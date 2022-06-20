using UnityEngine;

public partial class PlayerProgression
{
    public static int Level
    {
        get => PlayerPrefs.GetInt("level", 1);
        set => PlayerPrefs.SetInt("level", value);
    }

    public static int Coin
    {
        get => PlayerPrefs.GetInt("coin", 0);
        set => PlayerPrefs.SetInt("coin", value);
    }
}