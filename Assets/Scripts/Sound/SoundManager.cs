using UnityEngine;
using System.Collections;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    private SoundParameter _param;

    protected override void Awake()
    {
        base.Awake();

        _param = Resources.Load<SoundParameter>("ScriptableObjects/SoundParameter");

        Debug.Log(_param);
    }
}
