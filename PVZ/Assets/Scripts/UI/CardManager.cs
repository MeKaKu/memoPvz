using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<PlantAssetId> cardIds;//卡片数据列表, 已拥有
    List<Card> cards = new List<Card>();//卡片列表
    public Card cardPrefab;//卡片预制体
    public Transform cardBar;//卡片存放的位置

    public Card chosenCard;//当前被选中的卡片
    public ChosePlantPlane chosePlantPlane;//悬浮显示选中的植物
    public int cardSlotSize = 6;

    //选择携带的植物相关
    public Transform allCardBar;//所有植物卡片存放的位置
    bool isChoosingPlant;//是否正在选择携带的植物
    public GameObject allPlantPlane;//所有植物面板
    List<Card> allCards = new List<Card>();//所有卡片
    public event System.Action onCompleteChoosePlant;
    void Start(){
        //GenerateCards();
    }

    void Update(){
        //TODO:卡片状态
        foreach(Card card in cards){
            if(!isChoosingPlant)
                card.EnoughSun(SunManager.sunNum >= card.sunCost);
            else card.EnoughSun(true);
        }
        if(Input.GetMouseButtonDown(1)){
            //鼠标右键按下
            if(chosenCard){
                chosenCard.isChosen = false;
                UndoChooseCard(chosenCard);
            }
        }
    }

    /// <summary>
    /// 生成卡片
    /// </summary>
    public void AutoGenerateCards(){
        foreach(var cardId in cardIds){
            cards.Add(GenerateCard(cardId));
        }
    }
    Card GenerateCard(PlantAssetId cardId){
        Card card = Instantiate<Card>(cardPrefab, cardBar);
        card.assetId = cardId;
        card.isSunEnough = true;
        card.onPointerEnter += OnCardPointerEnter;
        card.onPointerDown += OnCardPointerDown;
        return card;
    }
    public void ShowAllPlant(){
        isChoosingPlant = true;
        foreach(var cardId in cardIds){
            Card card = GenerateCard(cardId);
            card.transform.SetParent(allCardBar);
            allCards.Add(card);
        }
        allPlantPlane.SetActive(true);
    }
    public void EndChoosePlant(){
        allPlantPlane.SetActive(false);
        isChoosingPlant = false;
        onCompleteChoosePlant?.Invoke();
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
        if(isChoosingPlant){
            _card.infoText.text = LocalData.instance.GetPlantArticle(_card.assetId).sampleInfo;
        }
        if(_card.isCD || _card.isChosen || !_card.isSunEnough){
            return ;
        }
        _card.DoMouseHover();
    }
    void OnCardPointerDown(Chooseable chooseable){
        Card _card = chooseable as Card;
        if(isChoosingPlant){
            if(_card.transform.parent == cardBar){
                //TODO:放回
                foreach(var card in allCards){
                    if(card.assetId == _card.assetId){
                        card.EnoughSun(true);
                    }
                }
                cards.Remove(_card);
                Destroy(_card.gameObject);
            }
            else{
                if(_card.isSunEnough == false || cards.Count >= cardSlotSize) return;
                //TODO:携带
                cards.Add(GenerateCard(_card.assetId));
                _card.EnoughSun(false);
            }
            return;
        }
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
