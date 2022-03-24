using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChosePlantPlane : BaseUI
{
    public RectTransform infoRect;
    public Image image;
    public AssetId assetId = AssetId.None;
    public LayerMask layerMask;
    private void Update() {
        if(assetId != AssetId.None){
            infoRect.anchoredPosition = Input.mousePosition;
            if(Input.GetMouseButtonDown(0)){
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray,out hit, layerMask)){
                    MapGenerator mapGenerator = FindObjectOfType<MapGenerator>();
                    mapGenerator.GeneratePlant(assetId, hit.point);
                    Hide();
                    CardManager cardManager = FindObjectOfType<CardManager>();
                    cardManager.chosenCard.isChosen = false;
                    cardManager.chosenCard = null;
                }
            }
        }
    }

    public void ShowPlant(AssetId _assetId){
        assetId = _assetId;
        image.sprite = LocalData.instance.GetPlantArticle(assetId).iconSprite;
        Show();
    }

    public override void Hide()
    {
        base.Hide();
        assetId = AssetId.None;
    }
}
