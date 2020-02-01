using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;
using System;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private TitleFadeView fadeView;
    [SerializeField] TitleButtonTextView buttonTextView;
    void Start ()
    {
        Observable.Timer(TimeSpan.FromMilliseconds(500))
        .Subscribe(_ =>
            fadeView.Fade(0)
        )
        .AddTo(this);

        Observable.Timer(TimeSpan.FromMilliseconds(1100))
        .Subscribe(_ =>
                this.UpdateAsObservable()
                    .Where(q => Input.anyKeyDown)
                    .First()
                    .Subscribe(w => {
                        SoundManager.PlaySound(SoundID.GameStartPush);
                        fadeView.Fade(1);
                        Observable.Timer(TimeSpan.FromMilliseconds(1100))
                            .Subscribe(e =>
                                //SceneManager.LoadScene("Game")
								SceneChanger.Instance.ChangeScene(SceneType.Synopsis)
                            )
                            .AddTo(this);
                    })
        )
        .AddTo(this);

    }




}
