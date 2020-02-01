using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵マネージャ
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    [SerializeField] private Transform _spawnTarget;

    private List<Enemy> _enemies = new List<Enemy>();

    private EnemyData _param;

    /// <summary>
    /// 敵生成
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Enemy SpawnEnemy(Vector2 position)
    {
        var enemy = Instantiate(_enemy, position, Quaternion.identity, _spawnTarget);
        _enemies.Add(enemy);

        enemy.Initialize(_param);
        enemy.GetComponent<EnemyMover>()?.Initialize(_param);

        return enemy;
    }

    private void Awake()
    {
        _param = Resources.Load<EnemyData>("ScriptableObjects/EnemyParameter");
    }

    #region テストコード

#if UNITY_EDITOR
    [ContextMenu("Spawn Enemy")]
    private void TestSpawnEnemy()
    {
        SpawnEnemy(new Vector2(
            UnityEngine.Random.Range(-5, 5),
            UnityEngine.Random.Range(-5, 5)
        ));
    }
#endif

    #endregion
}
