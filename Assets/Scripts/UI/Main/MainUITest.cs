using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MainUITest : MonoBehaviour
{

    public FloatReactiveProperty time = new FloatReactiveProperty(0);
    public IntReactiveProperty scorePoint = new IntReactiveProperty(0);
    public float staminaMax = 100;
    public float staminaConsumption = 100;
    public float staminaRecovery = 70;
    public FloatReactiveProperty staminaNow = new FloatReactiveProperty(0);
    public IntReactiveProperty lifePoint = new IntReactiveProperty(3);
    public StartCountView startCount;

    void Start()
    {
        staminaNow.Value = staminaMax;
        lifePoint.Value = 3;

        //startCount.StartCount();
    }

    void Update ()
    {
        
        time.Value -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            scorePoint.Value += 10;
        }


        if(Input.GetKey(KeyCode.Space))
        {
            staminaNow.Value = Mathf.Clamp(staminaNow.Value - staminaConsumption * Time.deltaTime, 0, staminaMax);
        }
        else if(staminaNow.Value < staminaMax)
        {
            staminaNow.Value = Mathf.Clamp(staminaNow.Value + staminaRecovery * Time.deltaTime, 0, staminaMax);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            lifePoint.Value--;
        }

    }
}
