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
    private Subject<Transform> _onFindPlayer = new Subject<Transform>();
    private Subject<Transform> _onMissPlayer = new Subject<Transform>();

    public IObservable<Unit> OnCollidePlayerAsObservable() => _onCollidePlayer;
    public IObservable<Transform> OnFindPlayerAsObjservable() => _onFindPlayer;
    public IObservable<Transform> OnMissPlayerAsObservable() => _onMissPlayer;

    public void Initialize(EnemyData param)
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
                _onFindPlayer.OnNext(collision.transform);
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // TODO : リテラル使わないようにしたい…
        switch ( collision.tag )
        {
            case "NearbyPlayer":
                _onMissPlayer.OnNext(collision.transform);
                break;
        }
    }
}
