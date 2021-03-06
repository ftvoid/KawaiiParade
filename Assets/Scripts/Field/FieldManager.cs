﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Random = UnityEngine.Random;

[RequireComponent(typeof(FieldInfomation))]
[RequireComponent(typeof(SetItemIncidence))]
public class FieldManager : MonoBehaviour
{
	enum SpawnType
	{
		NONE = -1,
		Item,
		Enemy
	}

	/// <summary>
	/// フィールドデータ情報
	/// </summary>
	private FieldInfomation _fieldInfo = null;

	[SerializeField]
	private ItemDatas _itemDatas = null;

	[SerializeField]
	private List<FieldInfomation> _field_datas = new List<FieldInfomation>();

	[SerializeField]
	private bool _isTestCreate = false;

	[SerializeField]
	private GameObject _testobj;

	private float _spawnTimer = 0.0f;

	private float _timer = 0.0f;

	private int _testNextSpawnPoint = 0;

	/// <summary>
	/// シングルトン
	/// </summary>
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
		if (_fieldInfo == null) _fieldInfo = this.GetComponent<FieldInfomation>();
		_fieldInfo.CreateMapDatas();
		if (!_isTestCreate)
		{
			//Spawn　Item
			SpawnObjects(_fieldInfo.Parameter.SpawnItemValue, SpawnType.Item);
		}
		else
		{
			TestCreateMapDatas();
		}

		GameState.Instance.R_Score
			.Where(_ => EnemyManager.Instance)
			.Where(x => x >= EnemyManager.Instance.NextSpawnScore)
			.Subscribe(x => SpawnEnemy()/*SpawnObjects(_fieldInfo.Parameter.SpawnEnemyValue, SpawnType.Enemy)*/)
			.AddTo(this);

		//テスト用
		//GameState.Instance.R_Score
		//			.Where(_ => GameState.Instance.Score >= _testNextSpawnPoint)
		//			.Subscribe(x => SpawnEnemy()/*SpawnObjects(_fieldInfo.Parameter.SpawnEnemyValue, SpawnType.Enemy)*/)
		//			.AddTo(this);
	}

	// Update is called once per frame
	void Update()
	{
		if ((ItemManager.Instance.ItemCount <= _fieldInfo.Parameter.MinExsitItemValue || _spawnTimer >= _fieldInfo.Parameter.SpawnIntarval) && GameManager.Instance.StartFlag.Value < 3)
		{
			List<int> ids = new List<int>();
			//マップデータ取得
			Vector2 size = _fieldInfo.GetFieldSize;
			var row = Random.Range(0, (int)size.x);
			var column = Random.Range(0, (int)size.y);
			var data = _fieldInfo.GetMapData(row, column);
			//すでにItemが配置されていたらcontinue
			if (data._isItem) return;
			//配置間隔を広げるため
			if (IsDiatance(ids, data._position, 4.0f)) return;
			//Item生成
			SpawnItem(data, row, column);
			if(_spawnTimer >= _fieldInfo.Parameter.SpawnIntarval) _spawnTimer = 0.0f;
		}
		_spawnTimer += Time.deltaTime;
	}

	/// <summary>
	/// オブジェクトの生成
	/// </summary>
	/// <param name="spawnValue"></param>
	private void SpawnObjects(int spawnValue, SpawnType spawnType)
	{
		int spawnCount = 0;
		List<int> ids = new List<int>();
		while (spawnCount < spawnValue)
		{
			_timer += Time.deltaTime;
			//マップデータ取得
			Vector2 size = _fieldInfo.GetFieldSize;
			var row = Random.Range(0, (int)size.x);
			var column = Random.Range(0, (int)size.y);
			var data = _fieldInfo.GetMapData(row, column);
			if (_timer >= 5.0f)
			{
				ids.Clear();
				_timer = 0.0f;
			}
			//すでにItemが配置されていたらcontinue
			if (data._isItem) continue;
			//配置間隔を広げるため
			if (IsDiatance(ids, data._position, 5.0f)) continue;
			ids.Add(data._id);
			if (spawnType == SpawnType.Item)
			{
				//Item生成
				SpawnItem(data, row, column);
			}
			else if (spawnType == SpawnType.Enemy)
			{
				//Enemy生成
				SpawnEnemy(data);
			}

			spawnCount++;
		}
	}



	/// <summary>
	/// アイテム生成
	/// </summary>
	/// <param name="data"> マップデータ </param>
	/// <param name="row"></param>
	/// <param name="column"></param>
	private void SpawnItem(MapData data, int row, int column)
	{
		data._isItem = true;
		_fieldInfo.SetMapData(row, column, data);
		var item = SetItemIncidence.Instance.GetRandomItemData();
		int num = Random.Range(0, _itemDatas.ColorList.Count);
		item.color = _itemDatas.ColorList[num];
		ItemManager.Instance.spawnItem(item, data._position, data._id);
	}

	/// <summary>
	/// Enenmyの生成
	/// </summary>
	/// <param name="data"></param>
	private void SpawnEnemy(MapData data)
	{
		EnemyManager.Instance.SpawnEnemy(data._position);
		//Instantiate(_enemyPrefab, data._position, Quaternion.identity, transform);
	}

	/// <summary>
	/// Enenmyの生成
	/// </summary>
	private void SpawnEnemy()
	{
		Vector2 playerPos = GameState.Instance.Player.transform.position;
		Vector2 spawnPos = Vector2.zero;
		float maxRange = 5.0f;
		bool flag = false;
		for (int i = 0; i < 10; i++)
		{
			//マップデータ取得
			Vector2 size = _fieldInfo.GetFieldSize;
			var row = Random.Range(0, (int)size.x);
			var column = Random.Range(0, (int)size.y);
			var data = _fieldInfo.GetMapData(row, column);
			spawnPos = data._position;
			float dis = Vector2.Distance(playerPos, data._position);
			if (dis > maxRange && !flag)
			{
				maxRange = dis;
				//_testNextSpawnPoint += 20000;
				//Instantiate(_testobj, spawnPos, Quaternion.identity);
				EnemyManager.Instance.SpawnEnemy(spawnPos);
				flag = true;
				break;
			}
		}
		//もし10回回しても生成されなければ最後に保持した座標で生成
		if (!flag)
		{
			//テスト
			//Instantiate(_testobj, spawnPos, Quaternion.identity);
			EnemyManager.Instance.SpawnEnemy(spawnPos);
		}
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
			var d = _fieldInfo.SearchMapData(num);
			dis = Vector2.Distance(pos, new Vector2(d._position.x, d._position.y));
			if (dis <= range) return true;

		}
		return false;
	}

	/// <summary>
	///  MapDataの作成
	/// </summary>
	public void TestCreateMapDatas()
	{
		for (int i = 0; i < _fieldInfo.Parameter.FieldSizeHorizontal; i++)
		{
			for (int j = 0; j < _fieldInfo.Parameter.FieldSizeVertical; j++)
			{
				var x = i /*- (_fieldInfo.Parameter.FieldSizeHorizontal / 2)*/;
				var y = j - (_fieldInfo.Parameter.FieldSizeVertical / 2);
				var pos = new Vector3(x, y, 0) + this.transform.position;
				var obj = Instantiate(_testobj, pos, Quaternion.identity);
				var item = obj.GetComponent<Item>();
				item.MapID = _fieldInfo.GetMapData(i, j)._id;
			}
		}
	}
}
