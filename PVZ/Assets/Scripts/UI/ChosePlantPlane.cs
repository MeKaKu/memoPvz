using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChosePlantPlane : BaseUI
{
    public RectTransform infoRect;
    public Image image;
    private void Update() {
        infoRect.anchoredPosition = Input.mousePosition;
    }

    public void ShowPlant(Sprite sprite){
        image.sprite = sprite;
        Show();
    }

}
