using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemView : MonoBehaviour
{

    public TMP_Text[] texts;


    public void ItemUpdate(int _i, int _num)
    {
        texts[_i].text = _num.ToString();
    }
}
