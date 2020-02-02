using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utage;
using UtageExtensions;

public class UtageStarter : MonoBehaviour
{
    AdvEngine Engine { get { return this.GetComponentCacheFindIfMissing( ref engine); } }
    [SerializeField]
    AdvEngine engine;

    void Start()
    {
        Debug.Log("conversation start");
        StartCoroutine(CoTalk());
    }

        IEnumerator CoTalk(){

        // ここでジャンプするシナリオラベルを選択
        var param = SceneChanger.Instance.Param;
        var senario = param != null ? (string)param : "Happy";
        Engine.JumpScenario(senario);

        // 「宴」のシナリオ終了待ち
        while(!Engine.IsEndScenario)
        {
            // メッセージ送り
            if ( Input.GetButtonDown("Dash") )
                Engine.Page.InputSendMessage();

            yield return null;
        }

        // シナリオ再生後はクレジットシーンへ遷移
        SceneChanger.Instance.ChangeScene(SceneType.Credit);
    }
}
