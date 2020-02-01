using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingletonMonoBehaviour<ItemManager>
{
    [SerializeField]
    private GameObject _itemPrefab;

	/// <summary>
	/// フィールドにあるアイテムリスト
	/// </summary>
	private List<Item> _itemList = new List<Item>();
	public int ItemCount
	{
		get { return _itemList.Count; }
	}

	/// <summary>
	/// アイテム獲得リスト
	/// </summary>
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
				_itemList.Remove(item);
				_collectionList.Add(item.ItemData);
			}
		}
    }

    public void spawnItem(ItemData data, Vector2 position)
    {
        var item = Item.Create(_itemPrefab, data, position);
		_itemList.Add(item);
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
