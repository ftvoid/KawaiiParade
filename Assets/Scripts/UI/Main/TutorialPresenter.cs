using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TutorialPresenter : MonoBehaviour
{
    public MainUITest model;
    public TutorialView view;

    void Start ()
    {
        //model.lifePoint
        //    .SkipLatestValueOnSubscribe()
        //    .Subscribe(_ => view.LifeUpdate(_));
    }

    private void Update ()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            view.TutorialStart();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            view.TutorialEnd();
        }
    }

}
