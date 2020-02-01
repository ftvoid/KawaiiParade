using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text m_score_Text;
    [SerializeField] private RectTransform rect;
    private int score;

    public void ScoreUpdate (int _score)
    {
        DOTween.To(
            () => score,
            _ => score = _,
            _score,
            0.5f
        );
        
    }

    private void Update ()
    {
        m_score_Text.text = score.ToString("00000000");
    }


}
