using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunManager : MonoBehaviour
{
    #region 字段
    public float interval = 5;//太阳生成间隔
    public GameObject sunPrefab;//太阳预制体
    public Transform suns;//太阳的父物体
    public Vector2 spawnRangeX = new Vector2(-425, 425);//太阳生成位置X坐标范围
    public Vector2 spawnRangeY = new Vector2(-275, 275);//太阳生成位置Y坐标范围
    public Transform sunCollection;//太阳被收集的位置
    float nextSpawnTime;//下一次生成太阳的时间
    public int startSunNum = 50;
    public static int sunNum;//当前的阳光值
    public Text sunNumText;
    #endregion

    private void Start() {
        sunNum = 0;
        nextSpawnTime = float.MaxValue;
        sunNumText.text = sunNum.ToString();
    }
    /// <summary>
    /// 开始生成阳光
    /// </summary>
    public void StatGenerateSun(){
        sunNum = startSunNum;
        sunNumText.text = sunNum.ToString();
        nextSpawnTime = Time.time + interval;
    }

    private void Update() {
        ListenAutoSpawn();
    }

    private void ListenAutoSpawn(){
        if(Time.time > nextSpawnTime){
            nextSpawnTime = Time.time + interval;
            Vector2 toPos = new Vector2(Random.Range(spawnRangeX.x, spawnRangeX.y),
                                        Random.Range(spawnRangeY.x, spawnRangeY.y - 100));
            Vector2 spawnPos = new Vector2(toPos.x, spawnRangeY.y);
            SpawnSun(spawnPos, toPos.y);
        }
    }

    public void SpawnSun(Vector2 spawnPos, float toPosY, float fallSpeed = .9f, float spawnTime = 0.1f){
        //获取太阳
        GameObject sunObject = GetSunObject();
        //设置太阳生成的位置
        RectTransform rect = sunObject.GetComponent<RectTransform>();
        rect.anchoredPosition = spawnPos;
        //太阳下落
        Sun sun = sunObject.GetComponent<Sun>();
        sun.InitSun();
        sun.toPosY = toPosY;
        sun.spawnTime = spawnTime;
        sun.fallSpeed = fallSpeed;
        sun.StartSpawn();
    }

    GameObject GetSunObject(){
        GameObject sunObject = null;
        if(suns.childCount <= 0 || suns.GetChild(0).gameObject.activeSelf){
            sunObject = Instantiate<GameObject>(sunPrefab, suns);
            Sun sun = sunObject.GetComponent<Sun>();
            sun.onCollectStarted += (num)=>{
                AudioManager.instance.PlaySound("CollectSun", Vector3.zero);
                sunNum += num;
            };
            sun.onSunCollected += ()=>{
                sunNumText.text = sunNum.ToString();
            };
        }
        else{
            sunObject = suns.GetChild(0).gameObject;
        }
        sunObject.SetActive(true);
        sunObject.transform.SetAsLastSibling();
        return sunObject;
    }

    public void UpdateSunNum(int _deltaSunNum){
        sunNum += _deltaSunNum;
        sunNumText.text = sunNum.ToString();
    }
}
