using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 2)]
public class EnemyData : ScriptableObject
{
    public Sprite sprite;

    public float collisionRadius = 0.4f;

    public float stopDuration = 1;
}