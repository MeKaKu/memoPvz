using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class Card : Chooseable
{
    Image image;//显示的图片
    CDMask cdMask;//cd遮罩
    public bool isSunEnough;//阳光是否足够
    public bool isCD{get;private set;}//是否正在CD
    public int sunCost{get;private set;}//需要花费的阳光
    private float cd;

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

    public void EnterCD(){
        cdMask.StartCD(cd);
    }
    public void EnoughSun(bool _isSunEnough){
        if(_isSunEnough && !isCD && !isChosen){
            image.color = Color.white;
        }
        else{
            image.color = Color.white * .75f;
        }
        isSunEnough = _isSunEnough;
    }

    public override void ShowInfo()
    {
        base.ShowInfo();
        if(isCD){
            infoText.text = "<color=#FF0000>recharging...</color>\n" + infoText.text;
        }
        else if(!isSunEnough){
            infoText.text = "<color=#FF0000>not enough sun</color>\n" + infoText.text;
        }
    }
}
