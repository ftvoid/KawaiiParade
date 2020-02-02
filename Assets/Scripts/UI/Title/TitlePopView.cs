using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using System;

public class TitlePopView : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] RectTransform[] rect;
    [SerializeField] float moveY = 1;
    [SerializeField] float tweenTime = 2;


    void Start()
    {



        Observable.Timer(TimeSpan.FromMilliseconds(2000))
        .Subscribe(_ => {
            Sequence Seq = DOTween.Sequence();
            Seq.Append(
                rect[0].DOLocalMoveY(moveY, tweenTime).SetEase(Ease.OutExpo)
            );
            Seq.Append(
                rect[0].DOLocalMoveY(0, tweenTime)
            );
            Seq.SetLoops(-1).SetDelay(2);
        })
        .AddTo(this);

        Observable.Timer(TimeSpan.FromMilliseconds(2500))
        .Subscribe(_ => {
            Sequence Seq2 = DOTween.Sequence();
            Seq2.Append(
                rect[1].DOLocalMoveY(moveY, tweenTime).SetEase(Ease.OutExpo)
            );
            Seq2.Append(
                rect[1].DOLocalMoveY(0, tweenTime)
            );
            Seq2.SetLoops(-1).SetDelay(2);
        })
        .AddTo(this);


    }


}
