using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FieldManager : MonoBehaviour
{
	/// <summary>
	/// フィールドサイズ（横）
	/// </summary>
	[SerializeField]
	private int _fieldSizeHorizontal = 10;
	/// <summary>
	/// フィールドサイズ（縦）
	/// </summary>
	[SerializeField]
	private int _fieldSizeVertical = 10;

	/// <summary>
	/// フィールドサイズの取得
	/// </summary>
	public Vector2 GetFieldSize
	{
		get { return new Vector2(_fieldSizeHorizontal, _fieldSizeVertical); }
	}

	/// <summary>
	/// アイテム生成数
	/// </summary>
	[SerializeField]
	private int _spawnItemValue = 5;

	[SerializeField]
	private GameObject _obje;

	[SerializeField]
	private List<FieldInfomation> _field_datas = new List<FieldInfomation>();

	public static FieldManager instance = null;

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
		CreateFieldDatas();
		SpawnObjects(_spawnItemValue,0);
	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// オブジェクトの生成
	/// </summary>
	/// <param name="spawnValue"></param>
	private void SpawnObjects(int spawnValue,int spawnType)
	{
		int spawnCount = 0;
		List<int> ids = new List<int>();
		while(spawnCount < spawnValue)
		{
			var row = Random.Range(0, _fieldSizeHorizontal);
			var column = Random.Range(0, _fieldSizeVertical);
			var data = GetFieldData(row, column);
			if (data._isItem) continue;
			if (IsDiatance(ids, data._position, 4.0f)) continue;
			data._isItem = true;
			ids.Add(data._id);
			SetFieldData(row, column, data);
			Instantiate(_obje, data._position, Quaternion.identity, transform);
			spawnCount++;
		}
	}

	/// <summary>
	///  FieldDataの作成
	/// </summary>
	private void CreateFieldDatas()
	{
		_field_datas = new List<FieldInfomation>();
		for (int i = 0; i < _fieldSizeHorizontal; i++)
		{
			var datas = new FieldInfomation();
			int num = 0;
			for (int j = 0; j < _fieldSizeVertical; j++)
			{
				var x = i - (_fieldSizeHorizontal / 2);
				var y = j - (_fieldSizeVertical / 2);
				FieldData data = new FieldData();
				var pos = new Vector3(x, y, 0) + this.transform.position;
				data._position = pos;
				data._isItem = false;
				data._id = num + i * 10;
				datas._fieldDatas.Add(data);
				num++;
			}
			_field_datas.Add(datas);
		}
	}

	/// <summary>
	/// フィールドデータの取得
	/// </summary>
	/// <param name="row"> ステージ横の値 </param>
	/// <param name="column"> ステージ縦の値 </param>
	/// <returns></returns>
	public FieldData GetFieldData(int row,int column)
	{
		return _field_datas[row]._fieldDatas[column];
	}

	/// <summary>
	/// フィールドデータの設定
	/// </summary>
	/// <param name="row"> ステージ横の値 </param>
	/// <param name="column"> ステージ縦の値 </param>
	/// <returns></returns>
	public void SetFieldData(int row, int column,FieldData data)
	{
		_field_datas[row]._fieldDatas[column] = data;
	}

	/// <summary>
	/// IDでフィールドデータを取得
	/// </summary>
	/// <param name="id"></param>
	/// <returns> IDが一致しなければnullを返す </returns>
	public FieldData SearchFieldData(int id)
	{
		int num1 = id / 10;
		int num2 = id % 10;
		var data = _field_datas[num1]._fieldDatas[num2];
		if (data._id == id) return data;
		return null;
	}

	/// <summary>
	/// 指定した距離より大きいいか
	/// </summary>
	/// <param name="p1"></param>
	/// <param name="p2"></param>
	/// <param name="range"> 指定距離 </param>
	/// <returns></returns>
	private bool IsDiatance(List<int> ids, Vector2 pos, float range)
	{
		if (ids.Count == 0) return false;
		float dis;
		foreach (int num in ids)
		{
			var d = SearchFieldData(num);
			dis = Vector2.Distance(pos, new Vector2(d._position.x,d._position.y));
			if (dis <= range) return true;

		}
		return false;
	}
}
