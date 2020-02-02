using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class CreditManager : MonoBehaviour
{
    private void Awake()
    {
        this.UpdateAsObservable()
            .Where(_ => Input.anyKeyDown)
            .Subscribe(_ => SceneChanger.Instance.ChangeScene(SceneType.Title));
    }
}
