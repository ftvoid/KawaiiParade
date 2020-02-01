using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム進行管理
/// </summary>
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public void StartGame()
    {
        Debug.Log("ゲーム開始！");
    }

    protected override void Awake()
    {
        base.Awake();

        // 今は自動で開始
        StartGame();
    }
}
