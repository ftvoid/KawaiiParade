using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TutorialPresenter : MonoBehaviour
{
    public GameManager manager;
    public TutorialView view;

    void Start ()
    {
        manager.StartFlag
            .Where(_ => _ == 0)
            .Subscribe(_ => view.TutorialStart())
            .AddTo(this);

        this.UpdateAsObservable()
            .Where(_ => Input.anyKey && view.readyCount == 1)
            .First()
            .Subscribe(_ => view.Tutorial2())
            .AddTo(this);

        this.UpdateAsObservable()
            .Where(_ => Input.anyKey && view.readyCount == 2)
            .First()
            .Subscribe(_ => view.TutorialEnd())
            .AddTo(this);

        view.flag
            .SkipLatestValueOnSubscribe()
            .Where(_ => _)
            .First()
            .Subscribe(_ => manager.StartFlag.Value++)
            .AddTo(this);

    }


}
