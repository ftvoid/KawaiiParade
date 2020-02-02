using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ResultPresenter : MonoBehaviour
{
    public GameManager manager;
    public ResultView view;

    void Start ()
    {
        manager.StartFlag
            .Where(_ => _ == 3)
            .Subscribe(_ => view.ResultStart())
            .AddTo(this);

        this.UpdateAsObservable()
            .Where(_ => Input.anyKey && view.ready)
            .First()
            .Subscribe(_ => manager.StartFlag.Value++)
            .AddTo(this);


    }

}
