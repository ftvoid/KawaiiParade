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
    //3. ゲーム終了,
    //4. 結果シーンに遷移
    public IntReactiveProperty StartFlag = new IntReactiveProperty(0);
    [SerializeField] private GameObject m_fieldManager;
    [SerializeField] private GameObject m_enemyManager;

    [SerializeField] private Behaviour[] _initialLockComponents;

    [SerializeField] private GameObject _bgm;

    private bool _isGameStarted = false;
    private string _endingType;

    public void StartGame()
    {
        Debug.Log("ゲーム開始！");
        
        StartCoroutine(InvokeCountDown());
    }

    protected override void Awake()
    {
        base.Awake();

        // カウントダウン時に特定コンポーネントを無効化
        for(var i = 0 ; i < _initialLockComponents.Length ; ++i )
        {
            var component = _initialLockComponents[i];
            if ( component == null )
                continue;

            component.enabled = false;
        }

        GameState.Instance.R_RemainTime
            .Where(x => x <= 0)
            .Subscribe(_ => {
                // TODO : スコアをScriptableObjectから取得して判定
                if(GameState.Instance.R_Score.Value >= 70000 )
                {
                    Debug.Log("ハッピーEND");
                    _endingType = "Happy";
                }
                else
                {
                    Debug.Log("おわかれEND");
                    _endingType = "Goodbye";
                }
                StartFlag.Value = 3;
            });

        var player = FindObjectOfType<PlayerScript>();
        if ( player != null )
        {
            player.playerLife
                .Where(_ => _isGameStarted)
                .Where(x => x <= 0)
                .Subscribe(_ => {
                    Debug.Log("おじさんEND");
                    _endingType = "Gameover";
                    StartFlag.Value = 3;
                    //SceneChanger.Instance.ChangeScene(SceneType.Ending, "Gameover");
                });
        }

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
            //Debug.Log($"残り時間：{GameState.Instance.RemainTime}");
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
        _bgm?.SetActive(true);

        for ( var i = 0 ; i < _initialLockComponents.Length ; ++i )
        {
            var component = _initialLockComponents[i];
            if ( component == null )
                continue;

            component.enabled = true;
        }

        _isGameStarted = true;


        while(StartFlag.Value < 3)
        {
            yield return null;
        }

        //ここでおじさんとか止める処理
        m_enemyManager.SetActive(false);
        Debug.Log("ゲーム終了");


        while(StartFlag.Value < 4)
        {
            yield return null;
        }
        //何かボタンが押されたら、シーン遷移
        //ここでシーン遷移処理
        SceneChanger.Instance.ChangeScene(SceneType.Ending, _endingType);



    }
}
