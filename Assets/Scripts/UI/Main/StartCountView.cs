using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using System;
using TMPro;

public class StartCountView : MonoBehaviour
{

    [SerializeField] TMP_Text m_text;
    [SerializeField] RectTransform rect;
    public BoolReactiveProperty flag = new BoolReactiveProperty(false);

    public void StartCount()
    {
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown ()
    {
        yield return new WaitForSeconds(1);
        //TextFlash();
        //m_text.text = "5";
        //yield return new WaitForSeconds(0.5f);

        TextFlash();
        m_text.text = "4";
        SoundManager.PlaySound(SoundID.Pause);
        yield return new WaitForSeconds(0.5f);

        TextFlash();
        m_text.text = "3";
        SoundManager.PlaySound(SoundID.Pause);
        yield return new WaitForSeconds(0.5f);

        TextFlash();
        m_text.text = "2";
        SoundManager.PlaySound(SoundID.Pause);
        yield return new WaitForSeconds(0.25f);

        TextFlash();
        m_text.text = "1";
        SoundManager.PlaySound(SoundID.Pause);
        yield return new WaitForSeconds(0.25f);

        TextFlash();
        m_text.text = "START";
        flag.Value = true;
        SoundManager.PlaySound(SoundID.GameStartPush);
        yield return new WaitForSeconds(1);
        rect.DOScale(new Vector3(0, 0), 0);
    }

    void TextFlash()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(
             rect.DOScale(new Vector3(1.3f, 1.3f), 0.1f)
        );
        seq.Append(
            rect.DOScale(new Vector3(1, 1), 0.3f)
        );
    }


}
