using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;

/// <summary>
/// 敵の動き
/// </summary>
[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _minWalkDistance = 1;

    [SerializeField] private float _maxWalkDistance = 2;

    [SerializeField] private float _walkSpeed = 1;

    [SerializeField] private Rect _walkRange;

    private Enemy _enemy;
    private Tween _motion;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();

        _enemy
            .OnPauseAsObservable()
            .Subscribe(x => {
                if ( x )
                    PauseWalk();
                else
                    ResumeWalk();
            });

        InvokeRandomWalk();
    }

    private void InvokeRandomWalk()
    {
        var nextPos = new Vector2(
            UnityEngine.Random.Range(_walkRange.xMin, _walkRange.xMax),
            UnityEngine.Random.Range(_walkRange.yMin, _walkRange.yMax));

        _motion = transform
            .DOMove(nextPos, _walkSpeed)
            .SetEase(Ease.Linear)
            .SetSpeedBased()
            .Play()
            .OnComplete(InvokeRandomWalk);
    }

    private void PauseWalk()
    {
        if ( _motion == null )
            return;

        Debug.Log("徘徊を一時停止");

        _motion.Pause();
    }

    private void ResumeWalk()
    {
        if ( _motion == null )
            return;

        Debug.Log("徘徊を再開");

        _motion.Play();
    }
}
