using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetItemIncidence : SingletonMonoBehaviour<SetItemIncidence>
{
	private ItemDatas _itemeDatas = null;

	private List<int> _incidenceList = new List<int>();

	protected override void Awake()
	{
		base.Awake();
		_itemeDatas = Instantiate(Resources.Load<ItemDatas>("ScriptableObjects/ItemDatas"));
		_incidenceList.Clear();
		SetIndenceList(_itemeDatas);
	}
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public ItemData GetRandomItemData()
	{
		var incidenceDistoribution = GetIncidenceDistributionList(_incidenceList);

		int rdm = Random.Range(0, incidenceDistoribution.Count);
		return _itemeDatas.dataList[incidenceDistoribution[rdm]];
	}

	/// <summary>
	/// リストに確率を格納
	/// </summary>
	/// <param name="datas"></param>
	public void SetIndenceList(ItemDatas datas)
	{
		_incidenceList.Clear();
		foreach (var d in datas.dataList)
		{
			_incidenceList.Add(d.incidence);
		}
	}

	List<int> GetIncidenceDistributionList(List<int> incidences)
	{
		var incidenceList = new List<int>();

		int gcd = GCD(incidences);

		for (int i = 0, len = incidences.Count; i < len; i++)
		{
			int incidence = incidences[i] / gcd;

			for (int j = 0; j <= incidence; j++)
			{
				incidenceList.Add(i);
			}
		}
		return incidenceList;
	}

	int GCD(List<int> numbers)
	{
		return numbers.Aggregate(GCD);
	}

	int GCD(int a, int b)
	{
		return b == 0 ? a : GCD(b, a % b);
	}
}
