using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TimerPresenter : MonoBehaviour
{
    public MainUITest model;
    public TimerView view;

    void Start ()
    {
        model.time
            .SkipLatestValueOnSubscribe()
            .Subscribe(_ => view.TimerUpdate(_));
    }
}
