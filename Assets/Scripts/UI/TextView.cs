using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using KoganeUnityLib;


public class TextView : MonoBehaviour
{
	[SerializeField]
	private string m_text = "";
	[SerializeField]
	private TMP_Typewriter m_typewriter;

	[SerializeField]
	private float _time_secound = 1.0f;

	[SerializeField]
	private float _message_speed = 20.0f;

	readonly string text = "彼氏と彼女がケンカ。\n 彼氏が怒りにまかせ、彼女の私物をマンションの80階から\n地上のまき散らしてしまった！\nしかも、家を追い出されてしまう始末！\n \n ”お前のファッションセンスは全然理解できない！\nもっとかわいい女の子になってほしいんだよ！！”";

	// Start is called before the first frame update
	void Start()
	{
		Observable.Timer(TimeSpan.FromSeconds(_time_secound))
			.Subscribe(_ =>
				m_typewriter.Play(m_text, _message_speed, null)
			);
	}
}
