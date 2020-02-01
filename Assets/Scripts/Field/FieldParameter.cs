using UnityEngine;

[CreateAssetMenu(fileName = "FieldParameter", menuName = "ScriptableObjects/FieldParameter", order = 2)]
public class FieldParameter : ScriptableObject
{
	[SerializeField, Header("フィールドサイズ（横）")]
	private int _fieldSizeHorizontal = 16;
	public int FieldSizeHorizontal
	{
		get { return _fieldSizeHorizontal; }
	}

	[SerializeField, Header("フィールドサイズ（縦）")]
	private int _fieldSizeVertical = 8;
	public int FieldSizeVertical
	{
		get { return _fieldSizeVertical; }
	}
	[SerializeField, Header("アイテム生成数")]
	private int _spawnItemValue = 5;
	public int SpawnItemValue
	{
		get { return _spawnItemValue; }
	}

	[SerializeField, Header("フィールドに存在するアイテムの最低数")]
	private int _minExsitItemValue = 2;
	public int MinExsitItemValue
	{
		get { return _minExsitItemValue; }
	}

	[SerializeField, Header("Enemy生成数")]
	private int _spawnEnemyValue = 5;
	public int SpawnEnemyValue
	{
		get { return _spawnEnemyValue; }
	}
}
