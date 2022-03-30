using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Chooseable : BaseUI, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public bool isMouseEnter;//鼠标是否进入
    public bool isChosen;//是否被选中
    public event System.Action<Chooseable> onPointerEnter;
    public event System.Action<Chooseable> onPointerDown;

    public Text infoText;//鼠标悬停的提示信息
    public Texture2D cursorTex;//光标

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
