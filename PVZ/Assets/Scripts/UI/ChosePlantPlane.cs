using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChosePlantPlane : BaseUI
{
    public RectTransform infoRect;
    public Image image;
    public PlantAssetId assetId = PlantAssetId.None;
    public LayerMask layerMask;
    private void Update() {
        if(assetId != PlantAssetId.None){
            infoRect.anchoredPosition = Input.mousePosition;
            if(Input.GetMouseButtonDown(0)){
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray,out hit, layerMask)){
                    MapGenerator mapGenerator = FindObjectOfType<MapGenerator>();
                    mapGenerator.GeneratePlant(assetId, hit.point);
                    Hide();
                    Card card = FindObjectOfType<CardManager>().chosenCard;
                    SunManager.sunNum -= card.sunCost;
                    card.EnterCD();
                    card.isChosen = false;
                    card = null;
                }
            }
        }
    }

    public void ShowPlant(PlantAssetId _assetId){
        infoRect.anchoredPosition = Input.mousePosition;
        assetId = _assetId;
        image.sprite = LocalData.instance.GetPlantArticle(assetId).iconSprite;
        Show();
    }

    public override void Hide()
    {
        base.Hide();
        assetId = PlantAssetId.None;
    }
}
