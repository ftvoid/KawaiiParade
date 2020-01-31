using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inspecterで中身を確認したかったんよ
[System.Serializable]
public class FieldInfomation
{
	[SerializeField]
	public List<FieldData> _fieldDatas = new List<FieldData>();
}

[System.Serializable]
public class FieldData
{
	/// <summary>
	/// 座標
	/// </summary>
	public Vector3 _position = Vector3.zero;

	/// <summary>
	/// アイテムが置いてあるか
	/// </summary>
	public bool _isItem = false;
}
