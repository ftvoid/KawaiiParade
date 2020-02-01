using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ItemView : MonoBehaviour
{

    public TMP_Text[] texts;
    public RectTransform[] rect;


    public void ItemUpdate(int _i, int _num)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(
            rect[_i].DOScale(new Vector3(2, 2), 0.1f)
        );
        seq.Append(
            rect[_i].DOScale(new Vector3(1, 1), 0.4f)
        );
        texts[_i].text = _num.ToString();
    }
}
