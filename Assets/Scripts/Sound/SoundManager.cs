using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// サウンドマネージャ
/// </summary>
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    private SoundParameter _param;

    private Dictionary<SoundID, AudioSource> _sounds = new Dictionary<SoundID, AudioSource>();

    /// <summary>
    /// 指定されたIDのサウンドを再生する
    /// </summary>
    /// <param name="soundId"></param>
    public static void PlaySound(SoundID soundId)
    {
        if ( !Instance._sounds.TryGetValue(soundId, out var sound) )
            return;

        sound.Play();
    }

    // 初期化
    protected override void Awake()
    {
        base.Awake();

        _param = Resources.Load<SoundParameter>("ScriptableObjects/SoundParameter");
        if ( _param == null || _param._soundList == null )
            return;

        var audioSourceTarget = transform;

        for ( var i = 0 ; i < _param._soundList.Length ; ++i )
        {
            var param = _param._soundList[i];
            var clip = Resources.Load<AudioClip>(param.resourcePath);

            var obj = new GameObject("Sound_" + param.soundId);
            var audioSource = obj.AddComponent<AudioSource>();
            audioSource.clip = clip;

            obj.transform.SetParent(audioSourceTarget);

            _sounds.Add(param.soundId, audioSource);
        }
    }

    #region テストコード
#if UNITY_EDITOR
    [ContextMenu("TestPlaySound")]
    private void TestPlaySound()
    {
        StartCoroutine(OnTestPlaySound());
    }

    private IEnumerator OnTestPlaySound()
    {
        foreach(var sound in _sounds )
        {
            sound.Value.Play();
            yield return new WaitForSeconds(2);
        }
    }
#endif
    #endregion
}
