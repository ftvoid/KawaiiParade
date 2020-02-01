using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class LifePresenter : MonoBehaviour
{
    public MainUITest model;
    public LifeView view;

    void Start ()
    {
        model.lifePoint
            .SkipLatestValueOnSubscribe()
            .Subscribe(_ => view.LifeUpdate(_));
    }
}
