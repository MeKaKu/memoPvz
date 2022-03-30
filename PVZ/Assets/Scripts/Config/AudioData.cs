using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//音频数据
[CreateAssetMenu(menuName = "Data/AudioData")]
public class AudioData : ScriptableObject
{
    //音频列表
    public List<AudioGroup> audioGroups = new List<AudioGroup>();
}

[System.Serializable]
public class AudioGroup{
    public string audioID;//声音ID
    public List<AudioClip> audioClips = new List<AudioClip>();//每种声音的音频列表
}
