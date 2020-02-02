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
	Result,  		//リザルト
    Ending,         //エンディング
    Credit,         //クレジット
}

public class SceneChanger : SingletonMonoBehaviour<SceneChanger>
{
    [RuntimeInitializeOnLoadMethod]
    private static void CreateInstance()
    {
        var obj = new GameObject("SceneChanger");
        obj.AddComponent<SceneChanger>();
        DontDestroyOnLoad(obj);
    }

    public object Param { get; private set; }

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
    /// <param name="param"></param>
    public void ChangeScene(SceneType type, object param = null)
    {
        Param = param;
        SceneManager.LoadScene((int)type);
    }
}
