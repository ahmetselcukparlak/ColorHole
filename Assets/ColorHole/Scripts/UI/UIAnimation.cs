using System;
using DG.Tweening;
using UnityEngine;

public sealed class UIAnimation : MonoBehaviour
{
    public UIAnimationType animationType;
    public Transform target;
    public float firstValue;
    public float secondValue;
    public float duration;
    public AnimationCurve animationCurve;

    private void OnEnable()
    {
        if (Application.isPlaying)
        {
            Play();
        }
    }

    private void Play()
    {
        switch (animationType)
        {
            case UIAnimationType.ScaleY:
                target.localScale = new Vector3(1f, firstValue, 1f);
                target.DOScaleY(secondValue, duration).SetEase(animationCurve);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}