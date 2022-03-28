using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class Card : BaseUI, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public PlantAssetId assetId = PlantAssetId.None;
    Image image;//显示的图片
    CDMask cdMask;//cd遮罩
    public bool isSunEnough;//阳光是否足够
    public bool isMouseEnter;//鼠标是否进入
    public bool isCD{get;private set;}//是否正在CD
    public bool isChosen;//是否被选中
    public int sunCost{get;private set;}//需要花费的阳光
    public Text infoText;//鼠标悬停的提示信息
    public Texture2D cursorTex;//光标
    private float cd;

    public event System.Action<Card> onPointerEnter;
    public event System.Action<Card> onPointerDown;

    void Awake() {
        isSunEnough = true;
        image = GetComponent<Image>();
        cdMask = GetComponentInChildren<CDMask>();
        cdMask.onStartCD += ()=>{
            isCD = true;
        };
        cdMask.onEndCD += ()=>{
            isCD = false;
        };
    }
    private void Start() {
        PlantData.PlantArticle plantArticle = LocalData.instance.GetPlantArticle(assetId);
        image.sprite = plantArticle.cardSprite;
        cd = plantArticle.cardCD;
        sunCost = plantArticle.sunCost;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerDown?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseEnter = true;
        ShowInfo();
        onPointerEnter?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseEnter = false;
        UndoMouseHover();
        HideInfo();
    }

    public void DoMouseHover(){
        Cursor.SetCursor(cursorTex, Vector2.zero, CursorMode.Auto);
    }
    public void UndoMouseHover(){
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    public void ShowInfo(){
        infoText.gameObject.SetActive(true);
    }
    public void HideInfo(){
        infoText.gameObject.SetActive(false);
    }
    public void EnterCD(){
        cdMask.StartCD(cd);
    }
    public void EnoughSun(bool _isSunEnough){
        if(_isSunEnough){
            image.color = Color.white;
        }
        else{
            image.color = Color.white * .77f;
        }
        isSunEnough = _isSunEnough;
    }
}
