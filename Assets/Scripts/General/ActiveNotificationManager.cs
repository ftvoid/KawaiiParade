using UnityEngine;

/// <summary>
/// ゲームイベント
/// </summary>
public enum GameEventID
{
	None = -1,
	SetUp,
	CountDown,
	Play,
	Pause,
	End,

}

public class ActiveNotificationManager : MonoBehaviour
{
	/// <summary>
	/// シングルトン
	/// </summary>
	public static ActiveNotificationManager instance;

	public GameEventID GetEventID
	{
		get;
		private set;
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	// Start is called before the first frame update
	void Start()
	{
	}

	/// <summary>
	/// ゲーム内の通知
	/// </summary>
	/// <param name="id"> イベントID </param>
	public void Notification(GameEventID id)
	{
		GetEventID = id;
	}

}
