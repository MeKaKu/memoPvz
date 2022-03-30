using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//音频库
public class AudioLibrary : MonoBehaviour
{
    public List<AudioData> audioDatas;//声音数据
    Dictionary<string, List<AudioClip>> audioDictionary;//声音列表

    private void Awake() {
        //初始化audioDictionary
        audioDictionary = new Dictionary<string, List<AudioClip>>();
        foreach(var audioData in audioDatas){//将audioDatas中的声音数据都加入audioDictionary
            foreach(var audioGroup in audioData.audioGroups){
                audioDictionary.Add(audioGroup.audioID, audioGroup.audioClips);
            }
        }
    }

    //通过audioID获取音频
    public AudioClip GetAudioClipByID(string _audioID){
        if(audioDictionary.ContainsKey(_audioID)){//存在_audioID声音
            List<AudioClip> audioClips = audioDictionary[_audioID];
            int index = Random.Range(0, audioClips.Count);//随机返回一个音频
            return audioClips[index];
        }
        return null;
    }
}
