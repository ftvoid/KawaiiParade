using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// ゲーム進行管理
/// </summary>
public class GameManager : SingletonMonoBehaviour<GameManager>
{

    //後でenumにしたい
    public IntReactiveProperty StartFlag = new IntReactiveProperty(0);
    [SerializeField] private GameObject m_fieldManager;

    public void StartGame()
    {
        Debug.Log("ゲーム開始！");
        
        StartCoroutine(InvokeCountDown());

        
    }

    protected override void Awake()
    {
        base.Awake();

        // 今は自動で開始
        //StartGame();
        TutorialAndCountDown();
    }


    public void TutorialAndCountDown()
    {
        StartCoroutine("StartTutorial");
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


    private IEnumerator StartTutorial()
    {
        StartFlag.Value = 0;

        while(StartFlag.Value < 2)
        {
            yield return null;
        }

        m_fieldManager.SetActive(true);

    }


}
