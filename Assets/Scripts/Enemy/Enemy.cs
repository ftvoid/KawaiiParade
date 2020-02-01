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
    // TODO : フィールドの持たせ方整理
    [SerializeField] private float _stopDuration = 1;

    private void Initialize(EnemyData data)
    {
        // TODO : 初期化
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.tag != "Player" )
            return;

        // TODO : 一時停止する
        Debug.Log("徘徊を一時停止");

        Observable
            .Timer(TimeSpan.FromSeconds(_stopDuration))
            .Subscribe(_ => Debug.Log("徘徊を再開"))
            .AddTo(this);
    }
}
