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
        // TODO : リテラル使わないようにしたい…
        switch ( collision.tag )
        {
            case "Player":
                _onPause.OnNext(Unit.Default);
                break;

            case "NearbyPlayer":
                Debug.Log("NearbyPlayer");
                break;
        }
    }
}
