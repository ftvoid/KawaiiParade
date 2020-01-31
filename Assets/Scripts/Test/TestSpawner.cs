using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [SerializeField]
    private ItemData testData;

    // Start is called before the first frame update
    void Start()
    {
        ItemManager itemManager = Object.FindObjectOfType<ItemManager>();
        itemManager.spawnItem(testData, new Vector2(0, 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
