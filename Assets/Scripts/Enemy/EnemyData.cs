using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 2)]
public class EnemyData : ScriptableObject
{
    public float _minWalkDistance = 1;

    public float _maxWalkDistance = 2;

    public float _walkSpeed = 1;

    public float _stopDuration = 1;

    public int _spawnScoreInterval = 5000;
}