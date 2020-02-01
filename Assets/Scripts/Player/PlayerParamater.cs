using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerParamater", menuName = "ScriptableObjects/PlayerParamater", order = 2)]
public class PlayerParamater : ScriptableObject
{
    public float Speed
    {
        get
        {
            return speed;
        }
    }
    [SerializeField]
    private float speed;

    public float StaminaMax
    {
        get
        {
            return staminaMax;
        }
    }
    [SerializeField]
    private float staminaMax;

    public int Life{
        get
        {
            return life;
        }
    }
    [SerializeField]
    private int life;

    public float StopTime
    {
        get
        {
            return stopTime;
        }
    }
    [SerializeField]
    private float stopTime;

    public float DashSpeed
    {
        get
        {
            return dashSpeed;
        }
    }
    [SerializeField]
    private float dashSpeed;

    public float DashTime
    {
        get
        {
            return dashTime;
        }
    }
    [SerializeField]
    private float dashTime;

    public float InvincibleTime
    {
        get
        {
            return invincibleTime;
        }
    }
    [SerializeField]
    private float invincibleTime;
}
