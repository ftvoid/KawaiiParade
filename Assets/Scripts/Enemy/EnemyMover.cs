using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        InvokeRandomWalk();
    }

    private void InvokeRandomWalk()
    {
        var nextPos = new Vector2(
            UnityEngine.Random.Range(_walkRange.xMin, _walkRange.xMax),
            UnityEngine.Random.Range(_walkRange.yMin, _walkRange.yMax));

        transform
            .DOMove(nextPos, _walkSpeed)
            .SetEase(Ease.Linear)
            .SetSpeedBased()
            .Play()
            .OnComplete(InvokeRandomWalk);
    }
}
