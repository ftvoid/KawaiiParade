using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using KoganeUnityLib;
using UniRx;
using System;

public class ResultView : MonoBehaviour
{

    [SerializeField] CanvasGroup m_panel_CanvasGroup;
    [SerializeField] CanvasGroup m_text_CanvasGroup;
    [SerializeField] RectTransform m_rect;
    [SerializeField] private TMP_Typewriter[] m_typewriters;
    [SerializeField] private string[] m_texts;
    public BoolReactiveProperty flag = new BoolReactiveProperty(false);
    public bool ready = false;




    public void ResultStart ()
    {
        SoundManager.PlaySound(SoundID.Click);

        Debug.Log("リザルトオン");
        Sequence startSeq = DOTween.Sequence();
        startSeq.Append(
            m_panel_CanvasGroup.DOFade(0.5f, 1f)
        );

        Observable.Timer(TimeSpan.FromMilliseconds(1000))
            .Subscribe(_ => {
                m_typewriters[0].Play(m_texts[0], 30, null);
            })
            .AddTo(this);

        Observable.Timer(TimeSpan.FromMilliseconds(1300))
            .Subscribe(_ => {
                m_typewriters[1].Play(m_texts[1], 30, null);
            })
            .AddTo(this);

        Observable.Timer(TimeSpan.FromMilliseconds(2000))
            .Subscribe(_ => {
                m_typewriters[2].Play(m_texts[2], 3, null);
            })
            .AddTo(this);

        Observable.Timer(TimeSpan.FromMilliseconds(3500))
            .Subscribe(_ => {
                m_typewriters[3].Play(m_texts[3], 30, null);
                TextFlash();
                ready = true;
            })
            .AddTo(this);
    }



    void TextFlash ()
    {
        Sequence seqLoop;
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
