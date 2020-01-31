using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
	[SerializeField]
	private int _field_size_horizonta = 10;
	[SerializeField]
	private int _field_size_vertical = 10;

	[SerializeField]
	private GameObject _obje;

	// Start is called before the first frame update
	void Start()
	{
		CreateField();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void CreateField()
	{
		for (int i = 1; i < _field_size_horizonta; i++)
		{
			for (int j = 1; j < _field_size_vertical; j++)
			{
				var x = i - (_field_size_horizonta / 2);
				var y = j - (_field_size_vertical / 2);

				var pos = new Vector3(x,y,0) + this.transform.position;
				Instantiate(_obje, pos, Quaternion.identity,transform);
			}
		}
	}
}
