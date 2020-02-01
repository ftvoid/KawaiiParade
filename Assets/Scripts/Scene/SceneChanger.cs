using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
	NONE = -1,
	Title,			//タイトル
	Synopsis,		//あらすじ
	GamePlay,		//プレイシーン
	Result			//リザルト
}

public class SceneChanger : SingletonMonoBehaviour<SceneChanger>
{
	protected override void Awake()
	{
		base.Awake();
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// シーンの遷移
	/// </summary>
	/// <param name="type"></param>
	public void ChangeScene(SceneType type)
	{
		SceneManager.LoadScene((int)type);
	}
}
