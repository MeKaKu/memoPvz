using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Player player;//玩家生命体
    Spawner spawner;//僵尸生成器
    public PauseMenu pauseMenu;//暂停菜单
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
        //pauseMenu = FindObjectOfType<PauseMenu>();
        if(pauseMenu == null){
            throw new System.Exception("No PauseMenu");
        }
        pauseMenu.Hide();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            pauseMenu.Show();
            Time.timeScale = 0;
        }
    }

    void GameOver(){
        //

    }

    void GameWin(){
        //
    }

}
