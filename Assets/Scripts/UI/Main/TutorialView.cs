using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoganeUnityLib;
using DG.Tweening;
using UniRx;
using System;
using UnityEngine.UI;

public class TutorialView : MonoBehaviour
{
    [SerializeField] CanvasGroup m_panel_CanvasGroup;
    [SerializeField] CanvasGroup m_text_CanvasGroup;
    [SerializeField] RectTransform m_rect;
    [SerializeField] Image image;
    [SerializeField] Sprite tutorialSprite2;
    private readonly string m_text = "Next... \n - Push Any Button -";
    private readonly string m_text2 = "START... \n - Push Any Button -";
    [SerializeField] private TMP_Typewriter m_typewriter;
    public BoolReactiveProperty flag = new BoolReactiveProperty(false);
    public int readyCount = 0;

    Sequence seqLoop;

    public void TutorialStart()
    {
        SoundManager.PlaySound(SoundID.Click);

        Debug.Log("チュートリアルオン");
        Sequence startSeq = DOTween.Sequence();
        startSeq.Append(
            m_panel_CanvasGroup.DOFade(1, 0.2f)
        );
        startSeq.Append(
            m_rect.DOScaleX(1, 0.3f).SetEase(Ease.OutExpo)
        );
        startSeq.Join(
            m_rect.DOScaleY(0.02f, 0.3f).SetEase(Ease.OutExpo)
        );

        startSeq.Append(
            m_rect.DOScaleY(1, 0.3f).SetEase(Ease.OutExpo)
        );

        Observable.Timer(TimeSpan.FromMilliseconds(1400))
            .Subscribe(_ => {
                m_typewriter.Play(m_text, 30, null);
                readyCount = 1;
            })
            .AddTo(this);

        Observable.Timer(TimeSpan.FromMilliseconds(1800))
            .Subscribe(_ => {
                TextFlash();
            })
            .AddTo(this);
    }



    public void Tutorial2 ()
    {
        SoundManager.PlaySound(SoundID.Click);

        Debug.Log("チュートリアル2");
        image.sprite = tutorialSprite2;
        Sequence startSeq = DOTween.Sequence();
        startSeq.Append(
            m_rect.DORotate(new Vector3(0, 90, 0), 0.1f)
        );
        startSeq.Append(
            m_rect.DORotate(new Vector3(0, 0, 0), 0.5f)
        );

        Observable.Timer(TimeSpan.FromMilliseconds(200))
            .Subscribe(_ => {
                image.sprite = tutorialSprite2;
            })
            .AddTo(this);

        Observable.Timer(TimeSpan.FromMilliseconds(200))
            .Subscribe(_ => {
                m_typewriter.Play(m_text2, 30, null);
                readyCount = 2;
            })
            .AddTo(this);
    }



    public void TutorialEnd ()
    {
        SoundManager.PlaySound(SoundID.Click);

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
        textSeq.Append(
            m_panel_CanvasGroup.DOFade(0, 0.2f)
        );
        flag.Value = true;
    }





    void TextFlash()
    {
        seqLoop = DOTween.Sequence();
        seqLoop.Append(
            m_text_CanvasGroup.DOFade(0, 0.5f)
        );
        seqLoop.Append(
            m_text_CanvasGroup.DOFade(1, 0.5f)
        );
        seqLoop.SetLoops(-1);
    }


}
