using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TimerPresenter : MonoBehaviour
{
    public MainUITest model;
    public GameState state;
    public TimerView view;

    void Start ()
    {
        state.R_RemainTime
            .SkipLatestValueOnSubscribe()
            .Subscribe(_ => view.TimerUpdate(_));

        //model.time
        //.SkipLatestValueOnSubscribe()
        //.Subscribe(_ => view.TimerUpdate(_));
    }
}
