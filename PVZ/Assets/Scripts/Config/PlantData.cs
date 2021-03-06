using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlantData")]
public class PlantData : ScriptableObject
{
    public List<PlantArticle> plantArticles;
    
    [System.Serializable]
    public class PlantArticle{
        public PlantAssetId id;
        public Sprite cardSprite;
        public Sprite iconSprite;
        public Plant plantPrefab;
        public float cardCD;
        public int sunCost;
        public string sampleInfo;
    }
}
