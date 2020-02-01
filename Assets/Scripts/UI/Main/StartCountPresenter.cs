using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class StartCountPresenter : MonoBehaviour
{

    [SerializeField] private GameManager manager;
    [SerializeField] private StartCountView view;

    void Start ()
    {
        manager.StartFlag
            .Where(_ => _ == 1)
            .First()
            .Subscribe(_ => view.StartCount());

        view.flag
            .SkipLatestValueOnSubscribe()
            .Where(_ => _)
            .First()
            .Subscribe(_ => manager.StartFlag.Value++);

    }
}
