using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text m_score_Text;
    [SerializeField] private RectTransform rect;

    public void ScoreUpdate (float _score)
    {
        m_score_Text.text = _score.ToString();
        Sequence seq = DOTween.Sequence();
        seq.Append(
            rect.DOScale(new Vector3(1.3f, 1.3f), 0.1f)
        );
        seq.Append(
            rect.DOScale(new Vector3(1, 1), 0.7f)
        );
    }
}
