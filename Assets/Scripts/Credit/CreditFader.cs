using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CreditFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup _target;

    [SerializeField] private float _fadeSpeed = 1;

    private void Update()
    {
        _target.alpha += Mathf.Clamp01(_fadeSpeed * Time.deltaTime);
    }
}
