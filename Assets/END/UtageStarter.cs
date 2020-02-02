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
        Engine.JumpScenario("Happy");

        // 「宴」のシナリオ終了待ち
        while(!Engine.IsEndScenario)
        {
            yield return null;
        }

        // シナリオ再生後はタイトルシーンへ遷移
    }
}
