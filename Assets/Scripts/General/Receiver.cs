using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Receiver : MonoBehaviour
{
	/// <summary>
	/// コールバック
	/// </summary>
	private ReactiveProperty<GameEventID> m_event_callback = new ReactiveProperty<GameEventID>();
	public ReactiveProperty<GameEventID> EventCallBack
	{
		get { return m_event_callback; }
	}

	// Start is called before the first frame update
	void Start()
	{
		//メッセージ受信
		this.UpdateAsObservable()
			.Select(_ => ActiveNotificationManager.instance.GetEventID)
			.DistinctUntilChanged()
			.Subscribe(x => m_event_callback.Value = x);
	}

}
