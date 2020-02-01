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
    //0. チュートリアル
    //1. カウントダウン
    //2. ゲーム開始,FieldManagerをアクティブ
    public IntReactiveProperty StartFlag = new IntReactiveProperty(0);
    [SerializeField] private GameObject m_fieldManager;

    [SerializeField] private GameObject _player;

    public void StartGame()
    {
        Debug.Log("ゲーム開始！");
        
        StartCoroutine(InvokeCountDown());
    }

    protected override void Awake()
    {
        base.Awake();

        GameState.Instance.R_RemainTime
            .Where(x => x <= 0)
            .Subscribe(_ => {
                // TODO : スコアをScriptableObjectから取得して判定
                if(GameState.Instance.R_Score.Value >= 5000 )
                {
                    Debug.Log("ハッピーEND");
                }
                else
                {
                    Debug.Log("おわかれEND");
                }
            });

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

        StartGame();

        // 各種スクリプト、オブジェクト有効化
        m_fieldManager.SetActive(true);

        if ( _player != null )
        {
            var player = _player.GetComponent<PlayerScript>();
            var playerMoveRestrict = _player.GetComponent<PlayerMoveRestrict>();

            if ( player != null )
                player.enabled = true;
            if ( playerMoveRestrict != null )
                playerMoveRestrict.enabled = true;
        }
    }
}
