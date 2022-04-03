using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Player player;//玩家生命体
    Spawner spawner;//僵尸生成器
    MenuManager menuManager;//UI

    [Header(">过关奖励")]
    public PlantAssetId awardPlant;//过关将得到的植物
    
    private void Start() {
        //初始化玩家
        player = FindObjectOfType<Player>();
        if(player == null){
            throw new System.Exception("No Player");
        }
        player.onDeath += GameOver;
        //初始化僵尸生成器
        spawner = FindObjectOfType<Spawner>();
        if(spawner == null){
            throw new System.Exception("No Spawner");
        }
        spawner.onNoWave += GameWin;
        //初始化暂停菜单
        menuManager = FindObjectOfType<MenuManager>();
        if(menuManager == null){
            throw new System.Exception("No MenuManager");
        }
        //GameOver();
        GameStart();
    }

    void GameStart(){
        //背景音乐
        AudioManager.instance.PlayMusic("GameBg01");
    }

    void GameOver(){
        //UI
        menuManager.ShowGameOver();
        //音效
        AudioManager.instance.PlaySound("EatHeadScream", Vector3.zero);
    }

    void GameWin(){
        //UI
        Vector2 screenPos = Camera.main.WorldToScreenPoint(spawner.lastDeadZombiePos);
        menuManager.DropCard(screenPos, awardPlant);
    }

}
