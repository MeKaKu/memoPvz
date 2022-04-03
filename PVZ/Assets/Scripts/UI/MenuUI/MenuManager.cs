using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public PauseMenu pauseMenu;//暂停菜单
    public Button pauseButton;//暂停按钮
    public GameOverPlane gameOverPlane;//游戏结束UI界面
    public GameWinPlane gameWinPlane;//游戏胜利UI界面
    private void Awake() {
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            pauseMenu.PauseGame();
        }
    }

    public void ShowGameOver(){
        gameOverPlane.Show();
    }

    public void ShowGameWin(){
        gameWinPlane.Show();
    }
}
