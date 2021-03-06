﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inspecterで中身を確認したかったんよ
[System.Serializable]
public class FieldInfomation : SingletonMonoBehaviour<FieldInfomation>
{
	/// <summary>
	/// フィールドのパラメータ
	/// </summary>
	private FieldParameter _param = null;
	public FieldParameter Parameter
	{
		get { return _param; }
	}

	/// <summary>
	/// フィールドサイズの取得
	/// </summary>
	public Vector2Int GetFieldSize
	{
		get { return new Vector2Int(_param.FieldSizeHorizontal, _param.FieldSizeVertical); }
	}

	/// <summary>
	/// 最小のマップID
	/// </summary>
	[SerializeField]
	private int _minMapID = 0;
	public int MinMapID
	{
		get { return _minMapID; }
	}

	/// <summary>
	/// 最大のマップID
	/// </summary>
	[SerializeField]
	private int _maxMapID = 0;
	public int MaxMapID
	{
		get { return _maxMapID; }
	}

	/// <summary>
	/// マップの最小座標
	/// </summary>
	public Vector3 MapMinPosition
	{
		get { return SearchMapData(_minMapID)._position; }
	}

	/// <summary>
	/// マップの最大座標
	/// </summary>
	public Vector3 MapMaxPosition
	{
		get { return SearchMapData(_maxMapID)._position; }
	}

	/// <summary>
	/// フィールドデータリスト
	/// </summary>
	[SerializeField]
	public List<MapDatas> _BigMapData = new List<MapDatas>();

	protected override void Awake()
	{
		base.Awake();
		_param = Resources.Load<FieldParameter>("ScriptableObjects/FieldParameter");
		_maxMapID = (_param.FieldSizeHorizontal - 1) * 10 + (_param.FieldSizeVertical - 1);
	}

	/// <summary>
	///  MapDataの作成
	/// </summary>
	public void CreateMapDatas()
	{
		_BigMapData = new List<MapDatas>();
		for (int i = 0; i < _param.FieldSizeHorizontal; i++)
		{
			var datas = new MapDatas();
			int num = 0;
			for (int j = 0; j < _param.FieldSizeVertical; j++)
			{
				var x = i; /*- (_param.FieldSizeHorizontal / 2);*/
				var y = j - (_param.FieldSizeVertical / 2);
				MapData data = new MapData();
				var pos = new Vector3(x, y, 0) + this.transform.position;
				data._position = pos;
				data._isItem = false;
				data._id = num + i * 10;
				datas._fieldDatas.Add(data);
				num++;
			}
			_BigMapData.Add(datas);
		}
	}

	/// <summary>
	/// マップデータの取得
	/// </summary>
	/// <param name="row"> ステージ横の値 </param>
	/// <param name="column"> ステージ縦の値 </param>
	/// <returns></returns>
	public MapData GetMapData(int row, int column)
	{
		return _BigMapData[row]._fieldDatas[column];
	}

	/// <summary>
	/// マップデータの設定
	/// </summary>
	/// <param name="row"> ステージ横の値 </param>
	/// <param name="column"> ステージ縦の値 </param>
	/// <returns></returns>
	public void SetMapData(int row, int column, MapData data)
	{
		_BigMapData[row]._fieldDatas[column] = data;
	}

	/// <summary>
	/// IDでマップデータを取得
	/// </summary>
	/// <param name="id"></param>
	/// <returns> IDが一致しなければnullを返す </returns>
	public MapData SearchMapData(int id)
	{
		int num1 = id / 10;
		int num2 = id % 10;
		var data = _BigMapData[num1]._fieldDatas[num2];
		if (data._id == id) return data;
		return null;
	}
}

[System.Serializable]
public class MapData
{
	/// <summary>
	/// マップID
	/// </summary>
	public int _id = 0;

	/// <summary>
	/// 座標
	/// </summary>
	public Vector3 _position = Vector3.zero;

	/// <summary>
	/// アイテムが置いてあるか
	/// </summary>
	public bool _isItem = false;
}

[System.Serializable]
public class MapDatas
{
	[SerializeField]
	public List<MapData> _fieldDatas = new List<MapData>();
}