using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using KoganeUnityLib;

public class TitleButtonTextView : MonoBehaviour
{
    private readonly string m_text = "GAME START\n - Push Any Button -";
    [SerializeField] private TMP_Typewriter m_typewriter;
    // Start is called before the first frame update
    void Start()
    {
        Observable.Timer(TimeSpan.FromMilliseconds(1000))
            .Subscribe(_ =>
                m_typewriter.Play(m_text, 20, null)
            );
    }

}
