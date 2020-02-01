using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TutorialPresenter : MonoBehaviour
{
    public MainUITest model;
    public GameManager manager;
    public TutorialView view;

    void Start ()
    {
        manager.StartFlag
            .Where(_ => _ == 0)
            .Subscribe(_ => view.TutorialStart());

        this.UpdateAsObservable()
            .Where(_ => Input.anyKey && view.ready)
            .First()
            .Subscribe(_ => view.TutorialEnd());

        view.flag
            .SkipLatestValueOnSubscribe()
            .Where(_ => _)
            .First()
            .Subscribe(_ => manager.StartFlag.Value++);

    }


}
