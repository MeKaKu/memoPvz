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
    public RectTransform bar;
    Vector2 originBarPos;
    private void Awake() {
        originBarPos = bar.anchoredPosition;
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

    public void DropCard(Vector2 pos, PlantAssetId plantAssetId){
        gameWinPlane.Show();
        gameWinPlane.ShowCard(pos, plantAssetId);
    }

    public void HideBar(){
        StartCoroutine(AnimateBar(3, originBarPos + Vector2.up * 200));
    }
    public void ShowBar(){
        StartCoroutine(AnimateBar(2, originBarPos));
    }

    IEnumerator AnimateBar(float duration, Vector2 to){
        float percent = 0;
        while(percent < 1){
            percent += Time.deltaTime / duration;
            bar.anchoredPosition = Vector2.Lerp(bar.anchoredPosition, to, percent);
            yield return null;
        }
    }
}
