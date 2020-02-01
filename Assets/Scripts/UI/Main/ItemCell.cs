using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCell : MonoBehaviour
{

    [SerializeField] private Image image;

    public void ImageUpdate(Sprite _sprite)
    {
        image.sprite = _sprite;
    }


}
