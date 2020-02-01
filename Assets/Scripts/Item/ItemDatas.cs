using UnityEngine;
using System.Collections.Generic;

//[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
//public class ItemData : ScriptableObject
//{
//    public Sprite sprite;

//    public float collisionRadius = 0.4f;

//    public int points;
//}

[CreateAssetMenu(fileName = "ItemDatas", menuName = "ScriptableObjects/ItemDatas", order = 1)]
public class ItemDatas : ScriptableObject
{
	public List<ItemData> dataList = new List<ItemData>();
}

[System.Serializable]
public class ItemData
{
	public Sprite sprite;

	public float collisionRadius = 0.4f;

	public int points;

	public Color color = Color.white;

	public float speedReta = 0.0f;
}