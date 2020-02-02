using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// ゲームの状態
/// </summary>
public class GameState : SingletonMonoBehaviour<GameState>
{
    /// <summary>
    /// ゲームのスコア
    /// </summary>
    [SerializeField]
    private IntReactiveProperty _score = new IntReactiveProperty(0);
    public int Score
    {
        get { return _score.Value; }
        set { _score.Value = value; }
    }
    public IntReactiveProperty R_Score
    {
        get { return _score; }
        set { _score = value; }
    }

    /// <summary>
    /// ゲームの残り時間[s]
    /// </summary>
    [SerializeField]
    private FloatReactiveProperty _remainTime = new FloatReactiveProperty(60);
    public float RemainTime
    {
        get => _remainTime.Value;
        set => _remainTime.Value = value;
    }
    public FloatReactiveProperty R_RemainTime
    {
        get => _remainTime;
        set => _remainTime = value;
    }

	/// <summary>
	/// プレイヤーオブジェクト
	/// </summary>
	[SerializeField]
	private GameObject _player = null;
	public GameObject Player
	{
		get { return _player; }
	}

	private void Start()
	{
		//あんましやりたくない方法
		//これでPlayerを取得しやすく
		_player = GameObject.FindGameObjectWithTag("Player");
	}
}
