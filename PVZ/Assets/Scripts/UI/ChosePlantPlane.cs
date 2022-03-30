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
        if(assetId != PlantAssetId.None){//手里面那里植物时
            infoRect.anchoredPosition = Input.mousePosition;
            if(Input.GetMouseButtonDown(0)){
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                MapGenerator mapGenerator = FindObjectOfType<MapGenerator>();
                if(Physics.Raycast(ray,out hit, layerMask)){
                    if(assetId == PlantAssetId.Shovel){//铲子
                        //TODO:铲除植物
                        mapGenerator.ClearPlant(hit.point);
                        Chooseable shovel = FindObjectOfType<ShovelManager>().shovel;
                        shovel.isChosen = false;
                        shovel.Show();
                    }
                    else{
                        Card card = FindObjectOfType<CardManager>().chosenCard;
                        if(mapGenerator.GeneratePlant(assetId, hit.point)){
                            FindObjectOfType<SunManager>().UpdateSunNum(-card.sunCost);
                            card.EnterCD();
                        }
                        card.isChosen = false;
                        card = null;
                    }
                    Hide();
                }
            }
        }
    }

    public void ShowPlant(PlantAssetId _assetId){
        infoRect.anchoredPosition = Input.mousePosition;
        assetId = _assetId;
        image.sprite = LocalData.instance.GetPlantArticle(assetId).iconSprite;
        Show();
        if(assetId == PlantAssetId.Shovel){
            AudioManager.instance.PlaySound("ChoseShovel", Vector3.zero);
        }
        else{
            AudioManager.instance.PlaySound("ChosePlant", Vector3.zero);
        }
    }

    public override void Hide()
    {
        base.Hide();
        assetId = PlantAssetId.None;
    }
}
