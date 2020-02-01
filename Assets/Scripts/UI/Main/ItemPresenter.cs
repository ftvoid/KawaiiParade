using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ItemPresenter : MonoBehaviour
{
    public ItemManager manager;
    public ItemView view;

    //どこがPresenterなの...

    //それぞれの個数がとれればいいけど、毎回コレクションを検索するとかはだめな気がする
    //こっちでも独立して個数を持つけど、それだと値に差異が生まれる可能性もあるかも
    [SerializeField] private int i1 = 0, i2 = 0, i3 = 0;

    public void ItemUpdate (bool _get, int _point)
    {
        int value = 0;
        if(_get) { value = 1; }
        else { value = -1; }

        //力で解決!
        //アイテムポイントが1000、2000、3000かで判断する
        if(_point == 1000)
        {
            i1 += value;
            view.ItemUpdate(0, i1);
        }
        else if(_point == 2000)
        {
            i2 += value;
            view.ItemUpdate(1, i2);
        }
        else if(_point == 3000)
        {
            i3 += value;
            view.ItemUpdate(2, i3);
        }
        else
        {
            Debug.Log("変なアイテムを拾った");
        }
    }


}
