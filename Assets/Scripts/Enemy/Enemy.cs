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
    private Subject<Unit> _onCollidePlayer = new Subject<Unit>();
    private Subject<Unit> _onFindPlayer = new Subject<Unit>();
    private Subject<Unit> _onMissPlayer = new Subject<Unit>();

    public IObservable<Unit> OnCollidePlayerAsObservable() => _onCollidePlayer;

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
                _onCollidePlayer.OnNext(Unit.Default);
                break;

            case "NearbyPlayer":
                _onFindPlayer.OnNext(Unit.Default);
                Debug.Log("プレイヤーを発見");
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // TODO : リテラル使わないようにしたい…
        switch ( collision.tag )
        {
            case "NearbyPlayer":
                _onMissPlayer.OnNext(Unit.Default);
                Debug.Log("プレイヤーを見失った");
                break;
        }
    }
}
