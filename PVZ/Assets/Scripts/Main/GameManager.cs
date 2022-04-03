using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Player player;//玩家生命体
    Spawner spawner;//僵尸生成器
    MenuManager menuManager;//UI
    
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
    }

    

    void GameOver(){
        //UI
        menuManager.ShowGameOver();
    }

    void GameWin(){
        //UI
        menuManager.ShowGameWin();
    }

}
