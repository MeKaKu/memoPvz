using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class Card : BaseUI, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    Image image;//显示的图片
    CDMask cdMask;//cd遮罩
    public bool isSunEnough;//阳光是否足够
    public bool isMouseEnter;//鼠标是否进入
    public bool isCD{get;private set;}//是否正在CD
    public bool isChosen;//是否被选中
    public int sunCost;//需要花费的阳光
    public Sprite cardSprite;//卡片的贴图
    public Sprite plantSprite;//植物的贴图
    public Text infoText;//鼠标悬停的提示信息
    public Texture2D cursorTex;//光标

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
        image.sprite = cardSprite;
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
}
