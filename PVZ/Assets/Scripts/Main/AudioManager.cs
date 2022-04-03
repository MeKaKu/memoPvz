using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//音频管理器
[RequireComponent(typeof(AudioLibrary))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;//单例模式

    AudioLibrary defaultAudioLibrary;//音频库
    AudioSource[] musicSources;//用来播放背景音乐的音源
    int currentMusicSourceIndex;//当前生效的音乐音源Index
    AudioSource soundSource2D;//平面音效音源

    //Transform player;//玩家位置

    public enum AudioType{ //声音类型
        Main,  //主声音
        Sound, //音效
        Music  //音乐
    };
    public float mainVolumePercent{get;private set;}//主声音音量
    public float soundVolumePercent{get;private set;}//音效音量
    public float musicVolumePercent{get;private set;}//背景音乐音量

    private void Awake() {
        if(instance == null){
            instance = this;//单例模式
            DontDestroyOnLoad(gameObject);//切换Scene时不摧毁对象

            //初始化变量
            defaultAudioLibrary = GetComponent<AudioLibrary>();
            musicSources = new AudioSource[2];//使用两个音源，实现切换背景音乐时的平滑过渡（淡入淡出）
            for(int i=0; i<2 ; i++){
                GameObject musicSourceObject = new GameObject("MusicSource"+i);//音源挂载对象
                musicSourceObject.transform.parent = transform;//层级
                musicSources[i] = musicSourceObject.AddComponent<AudioSource>();//音源组件
                musicSources[i].loop = true;//无限循环
                musicSources[i].playOnAwake = false;//不自动播放
            }
            GameObject soundSourceObject = new GameObject("SoundSource2D");//2D音效音源
            soundSourceObject.transform.parent = transform;
            soundSource2D = soundSourceObject.AddComponent<AudioSource>();

            //音量，获取用户对音量的设置
            mainVolumePercent = PlayerPrefs.GetFloat("MainVolumePercent", 1f);
            soundVolumePercent = PlayerPrefs.GetFloat("SoundVolumePercent", 1f);
            musicVolumePercent = PlayerPrefs.GetFloat("MusicVolumePercent", 1f);
        }
        else
            Destroy(gameObject);
    }

    //在指定位置播放音效
    public void PlaySound(AudioClip audioClip, Vector3 pos){
        if(audioClip != null){
            AudioSource.PlayClipAtPoint(audioClip, pos, mainVolumePercent * soundVolumePercent);
        }
    }
    //在指定位置播放音效，通过audioID指定音频
    public void PlaySound(string audioID, Vector3 pos, AudioLibrary audioLibrary = null){
        if(audioLibrary == null) audioLibrary = defaultAudioLibrary;
        AudioClip audioClip = audioLibrary.GetAudioClipByID(audioID);
        PlaySound(audioClip, pos);
    }
    //播放2D音效
    public void PlaySound(AudioClip audioClip){
        if(audioClip != null){
            soundSource2D.clip = audioClip;
            soundSource2D.Play();
        }
    }
    //播放指定音源
    public void PlaySound(AudioSource audioSource){
        audioSource.volume = mainVolumePercent * soundVolumePercent;
        audioSource.Play();
    }
    //指定音源，播放音效
    public void PlaySound(AudioSource audioSource, AudioClip audioClip){
        audioSource.clip = audioClip;
        PlaySound(audioSource);
    }
    //指定音源，通过audioID指定音频，播放音效
    public void PlaySound(AudioSource audioSource, string audioID, AudioLibrary audioLibrary = null){
        if(audioLibrary == null) audioLibrary = defaultAudioLibrary;
        AudioClip audioClip = audioLibrary.GetAudioClipByID(audioID);
        PlaySound(audioSource, audioClip);
    }
    
    //播放2D音效，通过audioID指定音频
    public void PlaySound(string audioID, AudioLibrary audioLibrary = null){
        if(audioLibrary == null) audioLibrary = defaultAudioLibrary;
        AudioClip audioClip = audioLibrary.GetAudioClipByID(audioID);
        PlaySound(audioClip);
    }

    //播放音乐
    public void PlayMusic(AudioClip audioClip, float fadeTime = 1f){
        if(audioClip == null) return;
        currentMusicSourceIndex ^= 1;//切换当前音乐音源
        musicSources[currentMusicSourceIndex].clip = audioClip;
        musicSources[currentMusicSourceIndex].Play();
        StartCoroutine(AnimateMusicCrossFade(fadeTime));
    }
    //播放音乐，通过audioID指定音频
    public void PlayMusic(string audioID, float fadeTime = 1f, AudioLibrary audioLibrary = null){
        if(audioLibrary == null) audioLibrary = defaultAudioLibrary;
        AudioClip audioClip = audioLibrary.GetAudioClipByID(audioID);
        PlayMusic(audioClip);
    }

    //音乐淡入淡出
    IEnumerator AnimateMusicCrossFade(float fadeTime){
        float percent = 0;
        float volumePercent = mainVolumePercent * musicVolumePercent;
        float speed = Time.deltaTime / fadeTime;
        while(percent < 1){
            yield return null;
            musicSources[currentMusicSourceIndex^1].volume = Mathf.Lerp(volumePercent, 0, percent);//上一个音乐音量淡出
            musicSources[currentMusicSourceIndex].volume = Mathf.Lerp(0, volumePercent, percent);//下一个音乐音量淡入
            percent += speed;
        }
        musicSources[currentMusicSourceIndex ^ 1].Stop();//停止播放上一个音乐
    }

    public void PauseMusic(){
        musicSources[currentMusicSourceIndex].Pause();
    }
    public void RecoverMusic(){
        musicSources[currentMusicSourceIndex].Play();
    }

    //修改音量
    public void SetVolume(float volumePercent, AudioType audioType){
        switch(audioType){
            case AudioType.Main:
                mainVolumePercent = volumePercent;
                PlayerPrefs.SetFloat("MainVolumePercent", mainVolumePercent);
                break;
            case AudioType.Sound:
                soundVolumePercent = volumePercent;
                PlayerPrefs.SetFloat("SoundVolumePercent", soundVolumePercent);
                break;
            case AudioType.Music:
                musicVolumePercent = volumePercent;
                PlayerPrefs.SetFloat("MusicVolumePercent", musicVolumePercent);
                break;
        }
        for(int i=0; i<2; i++){
            musicSources[i].volume = mainVolumePercent * musicVolumePercent;
        }
        PlayerPrefs.Save();
    }
}
