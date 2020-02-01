using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemManager : SingletonMonoBehaviour<ItemManager>
{
    [SerializeField]
    private GameObject _itemPrefab;

	/// <summary>
	/// フィールドにあるアイテムリスト
	/// </summary>
	[SerializeField]
	private List<Item> _itemList = new List<Item>();
	public int ItemCount
	{
		get { return _itemList.Count; }
	}

	/// <summary>
	/// アイテム獲得リスト
	/// </summary>
	[SerializeField]
	private List<ItemData> _collectionList = new List<ItemData>();
	public int CollectionCount
	{
		get { return _collectionList.Count; }
	}

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		List<Item> itemList = new List<Item>();
		itemList.AddRange(_itemList);

		foreach (var item in itemList)
		{
			if (item.IsCollection)
			{
				//スコア加算
				GameState.instance.Score += item.ItemData.points;
				_itemList.Remove(item);
				var mapData = FieldInfomation.instance.SearchMapData(item.MapID);
				mapData._isItem = false;
				int num1 = item.MapID / 10;
				int num2 = item.MapID % 10;
				FieldInfomation.Instance.SetMapData(num1, num2, mapData);
				var itemData = new ItemData();
				itemData.points = item.ItemData.points;
				itemData.color = item.ItemData.color;
				itemData.sprite = item.ItemData.sprite;
				_collectionList.Add(itemData);
				Destroy(item.gameObject);
			}
		}
    }

    public void spawnItem(ItemData data, Vector2 position,int mapId)
    {
        var item = Item.Create(_itemPrefab, data, position);
		item.MapID = mapId;
		_itemList.Add(item);
    }

	/// <summary>
	/// 一番新しいアイテムデータを返す
	/// </summary>
	/// <returns></returns>
	public ItemData GetItemData()
	{
		int num = _collectionList.Count - 1;
		return _collectionList[num];
	}

	/// <summary>
	/// アイテムを無くす
	/// </summary>
	public void LoseItem()
	{
		if (_collectionList.Count < 1) return;
		//int num = Random.Range(0, _collectionList.Count - 1);
		int num = _collectionList.Count - 1;
		_collectionList.RemoveAt(num);

	}

	/// <summary>
	/// 獲得したアイテムの合計ポイントを返す
	/// </summary>
	/// <returns></returns>
	public int GetTotalPoint()
	{
		int point = 0;
		foreach(var i in _collectionList)
		{
			point += i.points;
		}
		return point;
	}
}
