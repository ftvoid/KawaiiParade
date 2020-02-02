using UnityEngine;
using System.Collections;

public class CreditScroller : MonoBehaviour
{
    [SerializeField] private Transform _creditText;

    [SerializeField] private float _scrollSpeed = 100;

    private void Update()
    {
        var pos = _creditText.localPosition;
        pos += Vector3.up * _scrollSpeed * Time.deltaTime;
        _creditText.localPosition = pos;
    }
}
