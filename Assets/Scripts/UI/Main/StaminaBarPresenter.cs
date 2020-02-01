using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class StaminaBarPresenter : MonoBehaviour
{
    public PlayerScript player;
    public StaminaBarView view;

    void Start()
    {
        view.m_staminaMax = player.maxStamina;
        player.nowStamina
            .SkipLatestValueOnSubscribe()
            .Subscribe(_ => view.BarUpdate(_));

    }

}
