using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<PlantAssetId> cardIds;//卡片数据列表
    List<Card> cards = new List<Card>();//卡片列表
    public Card cardPrefab;//卡片预制体
    public Transform cardBar;//卡片存放的位置

    public Card chosenCard;//当前被选中的卡片
    public ChosePlantPlane chosePlantPlane;//悬浮显示选中的植物
    void Start(){
        GenerateCards();
    }

    void Update(){
        //TODO:卡片状态
        foreach(Card card in cards){
            card.EnoughSun(SunManager.sunNum >= card.sunCost);
        }
        if(Input.GetMouseButtonDown(1)){
            //鼠标右键按下
            if(chosenCard){
                chosenCard.isChosen = false;
                UndoChooseCard(chosenCard);
            }
        }
    }

    void GenerateCards(){
        foreach(var cardId in cardIds){
            Card card = Instantiate<Card>(cardPrefab, cardBar);
            card.assetId = cardId;
            //card.cardSprite = LocalData.instance.GetPlantArticle(cardId).cardSprite;
            card.onPointerEnter += OnCardPointerEnter;
            card.onPointerDown += OnCardPointerDown;
            cards.Add(card);
        }
    }
    void DoChooseCard(Card _card){
        chosenCard = _card;
        chosePlantPlane.ShowPlant(_card.assetId);
    }
    void UndoChooseCard(Card _card){
        chosePlantPlane.Hide();
        chosenCard = null;
    }
    void OnCardPointerEnter(Chooseable chooseable){
        Card _card = chooseable as Card;
        if(_card.isCD || _card.isChosen || !_card.isSunEnough){
            return ;
        }
        _card.DoMouseHover();
    }
    void OnCardPointerDown(Chooseable chooseable){
        Card _card = chooseable as Card;
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
