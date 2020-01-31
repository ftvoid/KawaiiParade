using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public Sprite sprite;

    public float collisionRadius = 0.4f;

    public int points;
}