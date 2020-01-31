using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Randam = UnityEngine.Random;

public class FieldManager : MonoBehaviour
{
	[SerializeField]
	private int _fieldSizeHorizontal = 10;
	[SerializeField]
	private int _fieldSizeVertical = 10;

	/// <summary>
	/// フィールドサイズの取得
	/// </summary>
	public Vector2 GetFieldSize
	{
		get { return new Vector2(_fieldSizeHorizontal, _fieldSizeVertical); }
	}

	[SerializeField]
	private int _spawnItemValue = 5;

	[SerializeField]
	private GameObject _obje;

	[SerializeField]
	private List<FieldInfomation> _field_datas = new List<FieldInfomation>();


	// Start is called before the first frame update
	void Start()
	{
		CreateFieldDatas();
		SpawnObjects(_spawnItemValue);
	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// オブジェクトの生成
	/// </summary>
	/// <param name="spawnValue"></param>
	private void SpawnObjects(int spawnValue)
	{
		for(int i = 0;i < spawnValue; i++)
		{
			var row    = Random.Range(0, _fieldSizeHorizontal);
			var column = Random.Range(0, _fieldSizeVertical);
			var data = GetFieldData(row, column);
			data._isItem = true;
			SetFieldData(row, column, data);
			Instantiate(_obje, data._position, Quaternion.identity, transform);
		}
	}

	/// <summary>
	///  FieldDataの作成
	/// </summary>
	private void CreateFieldDatas()
	{
		_field_datas = new List<FieldInfomation>();
		for (int i = 0; i <= _fieldSizeHorizontal; i++)
		{
			var datas = new FieldInfomation();
			for (int j = 0; j <= _fieldSizeVertical; j++)
			{
				var x = i - (_fieldSizeHorizontal / 2);
				var y = j - (_fieldSizeVertical / 2);
				FieldData data = new FieldData();
				var pos = new Vector3(x, y, 0) + this.transform.position;
				data._position = pos;
				data._isItem = false;
				datas._fieldDatas.Add(data);
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
}
