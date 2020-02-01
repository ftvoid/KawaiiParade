using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームの状態
/// </summary>
public class GameState : SingletonMonoBehaviour<GameState>
{
	/// <summary>
	/// ゲームのスコア
	/// </summary>
	[SerializeField]
	private int _score = 0;
	public int Score
	{
		get { return _score; }
		set { _score = value; }
	}

    /// <summary>
    /// ゲームの残り時間[s]
    /// </summary>
    [SerializeField]
    private int _remainTime = 60;
    public int RemainTime
    {
        get => _remainTime;
        set => _remainTime = value;
    }
}
