using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sun : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    #region 产生
    public bool isSpawning{get;private set;}//是否正在生成
    public float spawnTime = .5f;//生成时间
    private float spawnPercnt;//生成的进度百分比
    public void StartSpawn(){
        isSpawning = true;
        spawnPercnt = 0;
        if(spawnTime == 0){
            spawnPercnt = 2;
        }
    }
    private void ListenSpawn(){
        if(isSpawning){
            rect.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, spawnPercnt);
            if(spawnPercnt >= 1){
                isSpawning = false;
                StartFall();
            }
            else{
                spawnPercnt += Time.deltaTime/spawnTime;
            }
        }
    }
    #endregion

    #region 下落
    public bool isFalling{private set;get;}//是否正在下落
    public float fallSpeed = .9f;//下落速度
    public float toPosY = 0;//下落到的位置
    public void StartFall(){
        isFalling = true;
    }
    private void ListenFall(){
        if(isFalling){
            rect.anchoredPosition -= new Vector2(0, fallSpeed * Time.timeScale);
            if(rect.anchoredPosition.y <= toPosY){
                isFalling = false;
                StartExist();
            }
        }
    }
    #endregion

    #region 存在
    public bool isExisting{get;private set;}//是否存在
    public float existTime = 10;//存在时间
    private float existTimer = 0;//存在时间计时器
    public void StartExist(){
        isExisting = true;
        existTimer = 0;
    }
    private void ListenExist(){
        if(isExisting){
            existTimer += Time.deltaTime;
            if(existTimer > existTime){
                HideSun();
            }
        }
    }
    #endregion

    #region 收集
    public bool isCollecting{private set;get;}//是否正在收集
    public float collectTime = .8f;//收集太阳的时间
    public float collectPercent{get;private set;}//收集的进度百分比
    private Transform collectPos;//收集太阳的位置
    public event System.Action<int> onCollectStarted;//当太阳被使用（开始收集）
    public event System.Action onSunCollected;//当太阳被收集完成
    public void StartCollect(){
        isExisting = false;
        isCollecting = true;
        collectPercent = 0;
        collectPos = FindObjectOfType<SunManager>().sunCollection;
        onCollectStarted?.Invoke(numPerSun);
        if(collectTime == 0){
            collectPercent = 2;
        }
    }
    private void ListenCollect(){
        if(isCollecting){
            //位置
            rect.position = Vector3.Lerp(rect.position, collectPos.position, collectPercent);
            //大小
            if(collectPercent > .5f){
                rect.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, (collectPercent - .5f)/.5f);
                //GetComponent<Image>().CrossFadeAlpha(0, .2f, true);
            }
            if(collectPercent >= 1){
                isCollecting = false;
                onSunCollected?.Invoke();
                HideSun();
            }
            else{
                collectPercent += Time.deltaTime / collectTime;
            }
        }
    }

    private RectTransform rect;//太阳的位置
    public int numPerSun = 25;//太阳被收集时增加的阳光值

    
    #endregion

    public Texture2D cursorTex;//鼠标光标
    public void OnPointerDown(PointerEventData eventData)
    {
        if(isSpawning || isCollecting){
            return;
        }
        StartCollect();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isSpawning || isCollecting){
            return;
        }
        Cursor.SetCursor(cursorTex, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    private void Awake() {
        rect = GetComponent<RectTransform>();
    }
    private void Update() {
        ListenSpawn();
        ListenFall();
        ListenExist();
        ListenCollect();
    }
    public void InitSun(){
        isCollecting = false;
        isFalling = false;
        isSpawning = false;
        isExisting = false;
        //rect.localScale = Vector3.one;
    }
    public void HideSun(){
        rect.gameObject.SetActive(false);
        rect.SetAsFirstSibling();
    }
}
