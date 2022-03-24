using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalData : MonoBehaviour
{
    public PlantData plantData;
    Dictionary<AssetId, PlantData.PlantArticle> dictionary = new Dictionary<AssetId, PlantData.PlantArticle>();
    public static LocalData instance;
    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this);
            foreach(PlantData.PlantArticle plantArticle in plantData.plantArticles){
            dictionary.Add(plantArticle.id, plantArticle);
        }
        }
        else{
            Destroy(this);
        }
    }
    public PlantData.PlantArticle GetPlantArticle(AssetId id){
        Debug.Log(id);
        if(dictionary.TryGetValue(id, out PlantData.PlantArticle res)){
            return res;
        }
        return null;
    }
}
