using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class SynopsisManager : MonoBehaviour
{
	[SerializeField]
	private TitleFadeView fadeView;
	void Start()
	{
		Observable.Timer(TimeSpan.FromMilliseconds(500))
		.Subscribe(_ =>
			fadeView.Fade(0)
		).AddTo(this);

		Observable.Timer(TimeSpan.FromMilliseconds(1500))
		.Subscribe(_ =>
				this.UpdateAsObservable()
					.Where(q => Input.anyKeyDown)
					.First()
					.Subscribe(w =>
					{
                        SoundManager.PlaySound(SoundID.Click);
						fadeView.Fade(1);
						Observable.Timer(TimeSpan.FromMilliseconds(1500))
							.Subscribe(e => SceneChanger.Instance.ChangeScene(SceneType.GamePlay))
                            .AddTo(this);
					})
		)
        .AddTo(this);

	}
}
