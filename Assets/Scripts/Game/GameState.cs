using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームの状態
/// </summary>
public class GameState : SingletonMonoBehaviour<GameState>
{
    /// <summary>
    /// プレイヤーが移動できる範囲
    /// </summary>
    [SerializeField] private Rect _movableArea;

    public static Rect MovableArea => instance._movableArea;
}
