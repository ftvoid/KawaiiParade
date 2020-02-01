using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// ゲーム進行管理
/// </summary>
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public void StartGame()
    {
        Debug.Log("ゲーム開始！");
        StartCoroutine(InvokeCountDown());
    }

    protected override void Awake()
    {
        base.Awake();

        // 今は自動で開始
        StartGame();
    }




    private IEnumerator InvokeCountDown()
    {
        while ( GameState.Instance.RemainTime > 0 )
        {
            yield return new WaitForSeconds(1);
            --GameState.Instance.RemainTime;
            Debug.Log($"残り時間：{GameState.Instance.RemainTime}");
        }
    }
}
