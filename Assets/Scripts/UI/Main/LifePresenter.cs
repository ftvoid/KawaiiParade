using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class LifePresenter : MonoBehaviour
{
    public MainUITest model;
    public PlayerScript player;
    public LifeView view;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

        player.playerLife
            .SkipLatestValueOnSubscribe()
            .Subscribe(_ => view.LifeUpdate(_));

        //model.lifePoint
        //    .SkipLatestValueOnSubscribe()
        //    .Subscribe(_ => view.LifeUpdate(_));
    }
}
