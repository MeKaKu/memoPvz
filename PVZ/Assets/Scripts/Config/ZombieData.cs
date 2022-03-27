using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ZombieData")]
public class ZombieData : ScriptableObject
{
    public List<ZombieArticle> zombieArticles;
    
    [System.Serializable]
    public class ZombieArticle{
        public ZombieAssetId id;
        public Zombie zombiePrefab;
    }
}
