using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 敵
/// </summary>
public class Enemy : MonoBehaviour
{
    private Subject<Unit> _onPause = new Subject<Unit>();

    public IObservable<Unit> OnCollidePlayerAsObservable() => _onPause;

    private void Initialize(EnemyData data)
    {
        // TODO : 初期化
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.tag != "Player" )
            return;

        // TODO : 一時停止する
        _onPause.OnNext(Unit.Default);
    }
}
