using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlantData;
using static ZombieData;

public class LocalData : MonoBehaviour
{
    public PlantData plantData;
    public ZombieData zombieData;
    Dictionary<PlantAssetId, PlantArticle> plantDict = new Dictionary<PlantAssetId, PlantArticle>();
    Dictionary<ZombieAssetId, ZombieArticle> zombieDict = new Dictionary<ZombieAssetId, ZombieArticle>();
    public static LocalData instance;
    public int curLevel = 1;
    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this);
            //植物数据
            foreach(PlantArticle plantArticle in plantData.plantArticles){
                plantDict.Add(plantArticle.id, plantArticle);
            }
            //僵尸数据
            foreach(ZombieArticle zombieArticle in zombieData.zombieArticles){
                zombieDict.Add(zombieArticle.id, zombieArticle);
            }
        }
        else{
            Destroy(this);
        }
    }
    public PlantArticle GetPlantArticle(PlantAssetId id){
        if(plantDict.TryGetValue(id, out PlantArticle res)){
            return res;
        }
        return null;
    }
    public ZombieArticle GetZombieArticle(ZombieAssetId id){
        if(zombieDict.TryGetValue(id, out ZombieArticle res)){
            return res;
        }
        return null;
    }
    public Zombie GetZombiePrefab(ZombieAssetId id){
        if(zombieDict.TryGetValue(id, out ZombieArticle res)){
            return res.zombiePrefab;
        }
        return null;
    }
}
