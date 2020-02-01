using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleFadeView : MonoBehaviour
{
    [SerializeField] private CanvasGroup panel;
    [SerializeField] private float fadeTime = 1;
    public void Fade(int _fadeAlpha) 
    {
        panel.DOFade(_fadeAlpha, fadeTime);
    }
}
