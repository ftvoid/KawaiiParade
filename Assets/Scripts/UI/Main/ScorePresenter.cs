using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ScorePresenter : MonoBehaviour
{
    public GameState state;
    public ScoreView view;

    void Start ()
    {
        state.R_Score
            .SkipLatestValueOnSubscribe()
            .Subscribe(_ => view.ScoreUpdate(_));
    }
}
