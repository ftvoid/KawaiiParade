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
        );

        Observable.Timer(TimeSpan.FromMilliseconds(1500))
        .Subscribe(_ =>
                this.UpdateAsObservable()
                    .Where(q => Input.anyKeyDown)
                    .First()
                    .Subscribe(w => {
                        fadeView.Fade(1);
                        Observable.Timer(TimeSpan.FromMilliseconds(1500))
                            .Subscribe(e =>
                                //SceneManager.LoadScene("Game")
								SceneChanger.Instance.ChangeScene(SceneType.Synopsis)
                            );
                    })
        );

    }




}
