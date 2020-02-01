using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerView : MonoBehaviour
{
    public TMP_Text m_timer_Text;

    public void TimerUpdate(float _time)
    {
        m_timer_Text.text = _time.ToString("f0");
    }

}
