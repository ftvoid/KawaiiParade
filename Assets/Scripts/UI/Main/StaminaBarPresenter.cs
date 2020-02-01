using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class StaminaBarPresenter : MonoBehaviour
{
    public MainUITest model;
    public StaminaBarView view;

    void Start()
    {
        view.m_staminaMax = model.staminaMax;
        model.staminaNow
            .SkipLatestValueOnSubscribe()
            .Subscribe(_ => view.BarUpdate(_));
    }

}
