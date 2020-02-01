using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// 敵の動き
/// </summary>
[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _minWalkDistance = 1;

    [SerializeField] private float _maxWalkDistance = 2;

    [SerializeField] private float _walkSpeed = 1;

    [SerializeField] private float _stopDuration = 1;

    private Enemy _enemy;
    private Tween _motion;
    private Transform _targetPlayer;

    private enum State
    {
        None, RandomWalk, ChargeWalk, RandomWalkForce, Freeze,
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

        this.UpdateAsObservable()
            .Where(_ => _state == State.RandomWalk || _state == State.RandomWalkForce)
            .Subscribe(_ => OnUpdateRandomWalk());

        this.UpdateAsObservable()
            .Where(_ => _state == State.ChargeWalk)
            .Subscribe(_ => OnUpdateChargeWalk());

        _state = State.RandomWalk;
    }

    // ランダムウォーク実行
    private void InvokeRandomWalk()
    {
        _state = State.RandomWalk;

        var direction = UnityEngine.Random.Range(0, 2 * Mathf.PI);
        var distance = UnityEngine.Random.Range(_minWalkDistance, _maxWalkDistance);

        var nextPos = (Vector2)transform.position +
            new Vector2(Mathf.Cos(direction), Mathf.Sin(direction)) * distance;

        nextPos = ClampPosition(nextPos);

        _motion = transform
            .DOMove(nextPos, _walkSpeed)
            .SetEase(Ease.Linear)
            .SetSpeedBased()
            .Play()
            .OnComplete(InvokeRandomWalk);
    }

    private void OnUpdateRandomWalk()
    {
        //Debug.Log("ランダムウォーク");

        if ( _motion == null )
            InvokeRandomWalk();
    }

    private void OnUpdateChargeWalk()
    {
        //Debug.Log("追尾実行");

        if ( _motion != null )
        {
            _motion.Kill();
            _motion = null;
        }

        Vector2 dir = (_targetPlayer.position - transform.position).normalized;
        if ( Mathf.Approximately(dir.magnitude, 0) )
            dir = Vector2.up;

        var pos = transform.position;
        pos += (Vector3)dir * _walkSpeed * Time.deltaTime;
        transform.position = ClampPosition(pos);
    }

    // プレイヤーに接触した
    private void OnCollidePlayer()
    {
        if ( _state == State.Freeze )
            return;

        // 徘徊を一時停止
        if ( _motion != null )
        {
            _motion.Kill();
            _motion = null;
        }

        _state = State.Freeze;
        Debug.Log("徘徊を一時停止");

        Observable
            .Timer(TimeSpan.FromSeconds(_stopDuration))
            .Subscribe(_ =>
            {
                // 徘徊を再開
                ForceRandomWalk();
                Debug.Log("徘徊を再開");
            })
            .AddTo(this);
    }

    // 一定時間強制的にランダムウォーク
    private void ForceRandomWalk()
    {
        _state = State.RandomWalkForce;

        if ( _motion != null )
            _motion.Play();
        else
            InvokeRandomWalk();

        // 一定時間は追わないモードにする
        Observable
            .Timer(TimeSpan.FromSeconds(1))
            .Where(_ => _state == State.RandomWalkForce)
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

    private Vector2 ClampPosition(Vector2 position)
    {
        var min = FieldInfomation.Instance.MapMinPosition;
        var max = FieldInfomation.Instance.MapMaxPosition;

        return new Vector2(
            Mathf.Clamp(position.x, min.x, max.x),
            Mathf.Clamp(position.y, min.y, max.y));
    }
}
