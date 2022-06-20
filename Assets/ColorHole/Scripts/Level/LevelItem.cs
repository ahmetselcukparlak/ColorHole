using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[System.Serializable]
public class LevelItem
{
    [SerializeField] private Transform _baseObject;
    [SerializeField] private int _objectCount;
    [SerializeField] private bool _isOk;
    public int ObjectCount { get => _objectCount; set => _objectCount = value; }
    public bool IsOk { get => _isOk; set => _isOk = value; }

    private int _totalObjectCount;
    public void CheckObjectCount()
    {
        ObjectCount = _baseObject.childCount;
        _totalObjectCount = ObjectCount;
    }
    public void DecreateObjectCount()
    {
        ObjectCount--;
        var progressBar = UIManager.Instance.levelBar.GetComponent<UILevelProgressBar>();
        float result = (float)(_totalObjectCount - ObjectCount) * (float)(50 / _totalObjectCount);

        if (progressBar.Progress < 0.5f)
            progressBar.Progress = (result / 100);
        else
            progressBar.Progress = 0.5f + (result / 100);


        if (ObjectCount == 0)
        {
            IsOk = true;
            if (progressBar.Progress <= 0.5f)
                progressBar.Progress = 0.5f;
            else
                progressBar.Progress = 1f;
        }
    }
}
