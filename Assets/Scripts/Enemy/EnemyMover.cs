using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _minWalkDistance = 1;

    [SerializeField] private float _maxWalkDistance = 2;

    [SerializeField] private float _walkSpeed = 1;

    [SerializeField] private Rect _walkRange;

    private void Awake()
    {
        CreateRandomWalkMotion();
    }

    private void CreateRandomWalkMotion()
    {
        var nextPos = new Vector2(
            UnityEngine.Random.Range(_walkRange.xMin, _walkRange.xMax),
            UnityEngine.Random.Range(_walkRange.yMin, _walkRange.yMax));

        transform
            .DOMove(nextPos, _walkSpeed)
            .SetEase(Ease.Linear)
            .SetSpeedBased()
            .Play()
            .OnComplete(CreateRandomWalkMotion);
    }
}
