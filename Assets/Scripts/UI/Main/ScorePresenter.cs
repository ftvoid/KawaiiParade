using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ScorePresenter : MonoBehaviour
{
    public MainUITest model;
    public ScoreView view;

    void Start ()
    {
        model.scorePoint
            .SkipLatestValueOnSubscribe()
            .Subscribe(_ => view.ScoreUpdate(_));
    }
}
