using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoganeUnityLib;
using DG.Tweening;
using UniRx;
using System;

public class TutorialView : MonoBehaviour
{
    [SerializeField] CanvasGroup m_panel_CanvasGroup;
    [SerializeField] CanvasGroup m_text_CanvasGroup;
    [SerializeField] RectTransform m_rect;
    private readonly string m_text = "START... \n - Push Any Button -";
    [SerializeField] private TMP_Typewriter m_typewriter;
    public BoolReactiveProperty flag = new BoolReactiveProperty(false);
    public bool ready = false;

    Sequence seqLoop;

    public void TutorialStart()
    {
        Debug.Log("チュートリアルオン");
        Sequence startSeq = DOTween.Sequence();
        startSeq.Append(
            m_panel_CanvasGroup.DOFade(1, 0.5f)
        );
        startSeq.SetDelay(1);
        startSeq.Append(
            m_rect.DOScaleX(1, 0.3f).SetEase(Ease.OutExpo)
        );
        startSeq.Join(
            m_rect.DOScaleY(0.02f, 0.3f).SetEase(Ease.OutExpo)
        );

        startSeq.Append(
            m_rect.DOScaleY(1, 0.3f).SetEase(Ease.OutExpo)
        );

        Observable.Timer(TimeSpan.FromMilliseconds(3000))
            .Subscribe(_ =>
                m_typewriter.Play(m_text, 25, null)
            );

        Observable.Timer(TimeSpan.FromMilliseconds(4500))
            .Subscribe(_ => {
                TextFlash();
                ready = true;
            });
    }



    public void TutorialEnd ()
    {
        seqLoop.Kill();
        Sequence textSeq = DOTween.Sequence();
        textSeq.Append(
            m_rect.DOScaleY(0.02f, 0.3f)
        );
        textSeq.Append(
            m_rect.DOScaleX(0, 0.3f)
        );
        textSeq.Join(
            m_rect.DOScaleY(0, 0.3f)
        );
        textSeq.SetDelay(1);
        textSeq.Append(
            m_panel_CanvasGroup.DOFade(0, 0.5f)
        );
        flag.Value = true;
    }





    void TextFlash()
    {
        seqLoop = DOTween.Sequence();
        seqLoop.Append(
            m_text_CanvasGroup.DOFade(0, 1)
        );
        seqLoop.Append(
            m_text_CanvasGroup.DOFade(1, 1)
        );
        seqLoop.SetLoops(-1);
    }


}
