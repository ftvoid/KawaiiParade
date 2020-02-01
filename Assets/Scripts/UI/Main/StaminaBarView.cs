using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarView : MonoBehaviour
{

    [SerializeField] private Image m_bar;

    public float m_staminaMax;

    public void BarUpdate(float _stamina)
    {
        m_bar.rectTransform.localScale = new Vector3(Mathf.Clamp(_stamina/m_staminaMax, 0, 1), 1, 1);
    }



}
