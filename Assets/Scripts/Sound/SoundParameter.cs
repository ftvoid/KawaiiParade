using UnityEngine;

[CreateAssetMenu(fileName = "SoundParameter", menuName = "ScriptableObjects/SoundParameter", order = 3)]
public class SoundParameter : ScriptableObject
{
    [System.Serializable]
    public struct SoundInfo
    {
        public SoundID soundId;
        public string resourcePath;
    }

    public SoundInfo[] _soundList;
}