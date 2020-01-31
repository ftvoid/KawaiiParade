﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class Item : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private CircleCollider2D _collider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Create(GameObject prefab, ItemData data, Vector2 pos2d)
    {
        Vector3 pos3d = new Vector3(pos2d.x, pos2d.y, 0);
        GameObject obj = Instantiate(prefab, pos3d, Quaternion.identity);
        Item item = obj.GetComponent<Item>();
        item.initialize(data);
    }

    public void initialize(ItemData data)
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _collider = gameObject.GetComponent<CircleCollider2D>();
        _spriteRenderer.sprite = data.sprite;
        _collider.radius = data.collisionRadius;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // TODO: insert into player inventory
            Debug.Log("Item collided with player");
        }
    }
}