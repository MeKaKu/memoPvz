using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<AssetId> cardIds;//卡片数据列表
    List<Card> cards;//卡片列表
    public Card cardPrefab;//卡片预制体
    public Transform cardBar;//卡片存放的位置

    Card chosenCard;//当前被选中的卡片
    public ChosePlantPlane chosePlantPlane;//悬浮显示选中的植物
    ImageLibrary imageLibrary;//图片库
    void Start(){
        GenerateCards();
    }

    void Update(){
        if(Input.GetMouseButtonDown(1)){
            //鼠标右键按下
            if(chosenCard){
                chosenCard.isChosen = false;
                UndoChooseCard(chosenCard);
            }
        }
    }

    void GenerateCards(){
        imageLibrary = GetComponent<ImageLibrary>();
        foreach(var cardId in cardIds){
            Card card = Instantiate<Card>(cardPrefab, cardBar);
            card.cardSprite = imageLibrary.GetSprite(cardId);
            card.onPointerEnter += OnCardPointerEnter;
            card.onPointerDown += OnCardPointerDown;
        }
    }
    void DoChooseCard(Card _card){
        chosenCard = _card;
        chosePlantPlane.ShowPlant(_card.plantSprite);
    }
    void UndoChooseCard(Card _card){
        chosePlantPlane.Hide();
        chosenCard = null;
    }
    void OnCardPointerEnter(Card _card){
        if(_card.isCD || _card.isChosen || !_card.isSunEnough){
            return ;
        }
        _card.DoMouseHover();
    }
    void OnCardPointerDown(Card _card){
        if(_card.isCD || !_card.isSunEnough){//CD中或者阳光不够时,无法被选中
            return ;
        }
        _card.isChosen = !_card.isChosen;
        if(_card.isChosen){
            _card.UndoMouseHover();
            _card.HideInfo();
            DoChooseCard(_card);
        }
        else{
            _card.DoMouseHover();
            _card.ShowInfo();
            UndoChooseCard(_card);
        }
    }
}
