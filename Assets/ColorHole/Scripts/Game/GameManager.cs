using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseGameManager
{
    [SerializeField] private Transform FinishPos;
    public override void OnLevelStart()
    {
        base.OnLevelStart();
    }
    public override void OnLevelCompleted()
    {
        var WinFx = ObjectPooler.Instance.SpawnFromPool("WinFx", FinishPos.position, null, null);
        Level.ChangeStateRigidbody(false);
        Game.isGameOver = true;
        ObjectPooler.Instance.DestroyFromPool(WinFx, 1.5f);
        base.OnLevelCompleted();
        Invoke("NextLevel", 2f);
    }
    public override void OnLevelFailed()
    {
        base.OnLevelFailed();
    }

    public void NextLevel()
    {
        UIManager.Instance.OnNextButtonClick();
    }
}
