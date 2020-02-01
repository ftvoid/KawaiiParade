using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class LifeView : MonoBehaviour
{

    [SerializeField] private CanvasGroup[] m_life_canvas;
    [SerializeField] private float m_life_Lost_FlashCount = 3;
    public void LifeUpdate(int _life)
    {
        if(_life < 0) return;
        Sequence seq = DOTween.Sequence();
        for(var i = 0; i < m_life_Lost_FlashCount; i++)
        {
            seq.Append(
                    m_life_canvas[_life].DOFade(0, 0.1f)
                );
            seq.Append(
                    m_life_canvas[_life].DOFade(1, 0.1f)
                );
        }
        seq.Append(
            m_life_canvas[_life].DOFade(0, 0.1f)
        );


    }

}
