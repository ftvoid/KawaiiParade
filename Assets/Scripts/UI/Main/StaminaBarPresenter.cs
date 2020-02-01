using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class StaminaBarPresenter : MonoBehaviour
{
    public MainUITest model;
    public PlayerScript player;
    public StaminaBarView view;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        view.m_staminaMax = player.maxStamina;
        player.nowStamina
            .SkipLatestValueOnSubscribe()
            .Subscribe(_ => view.BarUpdate(_));

        //view.m_staminaMax = model.staminaMax;
        //model.staminaNow
        //    .SkipLatestValueOnSubscribe()
        //    .Subscribe(_ => view.BarUpdate(_));
    }

}
