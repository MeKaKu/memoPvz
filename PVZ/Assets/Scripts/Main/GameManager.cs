using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Player player;//玩家生命体
    Spawner spawner;//僵尸生成器
    MenuManager menuManager;//UI
    SunManager sunManager;//阳光系统
    CardManager cardManager;//植物卡片系统
    MapGenerator map;//地图
    Transform cameraTrans;//相机位置
    Vector3 originCameraPos;//相机初始位置

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
        //初始化阳光系统
        sunManager = FindObjectOfType<SunManager>();
        if(sunManager == null){
            throw new System.Exception("No SunManager");
        }
        //初始化植物卡片系统
        cardManager = FindObjectOfType<CardManager>();
        if(cardManager == null){
            throw new System.Exception("No CardManager");
        }
        //初始化地图生成器
        map = FindObjectOfType<MapGenerator>();
        if(map == null){
            throw new System.Exception("No MapGenerator");
        }
        //相机
        cameraTrans = Camera.main.transform;
        originCameraPos = cameraTrans.position;
        //GameOver();
        Invoke("GameStart", 1);
    }

    void GameStart(){
        //背景音乐
        AudioManager.instance.PlayMusic("ChoosePlantBg");
        StartCoroutine(AnimatePreview());
        menuManager.HideBar();
        spawner.PreviewZombie(2);
    }

    void DoChoosePlant(){
        if(cardManager.cardSlotSize < cardManager.cardIds.Count){
            //TODO:ChooseCard
        }
        else{
            cardManager.AutoGenerateCards();
            StartCoroutine( AnimateEndPreview(3) );
        }
    }

    void StartPlant(){
        //背景音乐
        AudioManager.instance.PlayMusic("GameBg01");
        map.GenerateMap();
        spawner.DelayStartSpawn(10);
        sunManager.StatGenerateSun();
        menuManager.ShowBar();
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

    IEnumerator AnimatePreview(){
        float percent = 0;
        float duration = .5f;
        while(percent < 1){
            percent += Time.deltaTime / duration;
            yield return null;
        }
        //左移
        percent = 0;
        Vector3 leftPos = new Vector3(0.930000007f,1.42999995f,-2.77699995f);
        duration = 1;
        while(percent < 1){
            percent += Time.deltaTime / duration;
            cameraTrans.position = Vector3.Lerp(originCameraPos, leftPos, percent);
            yield return null;
        }
        //停留
        percent = 0;
        duration = 2;
        while(percent < 1){
            percent += Time.deltaTime / duration;
            yield return null;
        }
        //右移
        Vector3 rightPos = new Vector3(4.03000021f,1.42999995f,-2.77699995f);
        percent = 0;
        duration = 3;
        while(percent < 1){
            percent += Time.deltaTime / duration;
            cameraTrans.position = Vector3.Lerp(leftPos, rightPos, percent);
            yield return null;
        }
        DoChoosePlant();
    }
    IEnumerator AnimateEndPreview(float delay){
        float percent = 0;
        while(percent < 1){
            percent += Time.deltaTime / delay;
            yield return null;
        }
        percent = 0;
        float duration = 2;
        Vector3 fromPos = cameraTrans.position;
        while(percent < 1){
            percent += Time.deltaTime / duration;
            cameraTrans.position = Vector3.Lerp(fromPos, originCameraPos, percent);
            yield return null;
        }

        //开始种植物吧
        StartPlant();
    }
}
