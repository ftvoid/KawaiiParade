using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class LifePresenter : MonoBehaviour
{
    public PlayerScript player;
    public LifeView view;

    //全然Presenterじゃない...
    void Start ()
    {
        player.playerLife
            .SkipLatestValueOnSubscribe()
            .Where(_ => _ < 3)
            .Subscribe(_ => view.LifeUpdate(_))
            .AddTo(this);
    }
}
