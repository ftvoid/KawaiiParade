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

    [SerializeField] private float _stopDuration = 1;

    private Enemy _enemy;
    private Tween _motion;
    private Transform _targetPlayer;

    private enum State
    {
        None, RandomWalk, ChargeWalk, Escape, Freeze,
    }

    [SerializeField] private State _state = State.None;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();

        _enemy
            .OnCollidePlayerAsObservable()
            .Subscribe(_ => OnCollidePlayer());

        _enemy
            .OnFindPlayerAsObjservable()
            .Subscribe(OnFindPlayer);

        _enemy
            .OnMissPlayerAsObservable()
            .Subscribe(OnMissPlayer);

        InvokeRandomWalk();
    }

    // ランダムウォーク実行
    private void InvokeRandomWalk()
    {
        _state = State.RandomWalk;

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

    // プレイヤー追尾実行
    private void InvokeChargeWalk()
    {

    }

    // プレイヤーに接触した
    private void OnCollidePlayer()
    {
        if ( _motion == null || _state == State.Freeze )
            return;

        // 徘徊を一時停止
        _motion.Pause();
        _state = State.Freeze;
        Debug.Log("徘徊を一時停止");

        Observable
            .Timer(TimeSpan.FromSeconds(_stopDuration))
            .Subscribe(_ =>
            {
                // 徘徊を再開
                Escape();

                Debug.Log("徘徊を再開");
            })
            .AddTo(this);
    }

    // 一定時間強制的にランダムウォーク
    private void Escape()
    {
        _state = State.Escape;

        if ( _motion != null )
            _motion.Play();
        else
            InvokeChargeWalk();

        // 一定時間は追わないモードにする
        Observable
            .Timer(TimeSpan.FromSeconds(1))
            .Where(_ => _state == State.Escape)
            .Subscribe(_ => _state = State.RandomWalk)
            .AddTo(this);
    }

    // プレイヤーを発見した
    private void OnFindPlayer(Transform target)
    {
        _targetPlayer = target;

        //_motion?.Kill();

        Debug.Log("プレイヤーを発見");
        _state = State.ChargeWalk;

        //InvokeChargeWalk();
    }

    // プレイヤーを見失った
    private void OnMissPlayer(Transform target)
    {
        _targetPlayer = null;

        //_motion?.Kill();

        Debug.Log("プレイヤーを見失った");
        _state = State.RandomWalk;

        //InvokeRandomWalk();
    }
}
