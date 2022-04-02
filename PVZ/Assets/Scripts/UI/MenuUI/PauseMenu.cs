using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : BaseUI
{
    public Slider[] sliders;
    private void Start() {
        //获取玩家的设置
        sliders[0].value = AudioManager.instance.mainVolumePercent;//主音量
        sliders[1].value = AudioManager.instance.musicVolumePercent;//音乐
        sliders[2].value = AudioManager.instance.soundVolumePercent;//音效
    }

    /// <summary>继续游戏
    /// 从暂停菜单返回游戏
    /// </summary>
    public void BackToGame(){
        Hide();
        Time.timeScale = 1;
    }
    /// <summary> 重新开始游戏
    /// 重新开始当前关卡
    /// </summary>
    public void RestartGame(){
        //TODO:restart
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    /// <summary>
    /// 返回主菜单
    /// </summary>
    public void BackToMenu(){
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    /// <summary>
    /// 设置游戏主音量
    /// </summary>
    /// <param name="volume">音量</param>
    public void SetMainVolume(float volume){
        AudioManager.instance.SetVolume(volume, AudioManager.AudioType.Main);
    }
    public void SetMusicVolume(float volume){
        AudioManager.instance.SetVolume(volume, AudioManager.AudioType.Music);
    }
    public void SetSoundVolume(float volume){
        AudioManager.instance.SetVolume(volume, AudioManager.AudioType.Sound);
    }
}
