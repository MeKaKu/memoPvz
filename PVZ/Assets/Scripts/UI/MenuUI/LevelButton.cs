using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class LevelButton : BaseUI, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ICanvasRaycastFilter
{
    Image image;
    RectTransform rect;
    Vector2 originPos;
    Sprite normalSprite;
    public Sprite hoverSprite;
    public float delayTime = 2;
    bool isClicked;//是否被点击过了
    private void Awake() {
        rect = GetComponent<RectTransform>();
        originPos = rect.anchoredPosition;
        image = GetComponent<Image>();
        normalSprite = image.sprite;
    }
    private void Start() {
        AudioManager.instance.PlayMusic("MainMenuBg");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        rect.anchoredPosition = originPos - Vector2.up * 5;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = hoverSprite;
        AudioManager.instance.PlaySound("ButtonHover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = normalSprite;
        rect.anchoredPosition = originPos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rect.anchoredPosition = originPos;
        isClicked = true;
        //TODO:按下button事件
        Invoke("ChangeSprite", .1f);
        Invoke("OnLevelButtonClicked", delayTime);
        AudioManager.instance.PlaySound("ZombieLaugh");
    }

    void ChangeSprite(){
        if(image.sprite == normalSprite){
            image.sprite = hoverSprite;
        }
        else{
            image.sprite = normalSprite;
        }
        Invoke("ChangeSprite", .1f);
    }

    public virtual void OnLevelButtonClicked(){
        CancelInvoke("ChangeSprite");
        SceneManager.LoadScene(1);
    }

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return !isClicked;
    }
}
