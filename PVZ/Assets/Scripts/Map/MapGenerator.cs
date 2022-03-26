using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Vector2 deltaSize;
    public Transform gridPrefab;//格子的预制体
    public Transform floor;
    public LayerMask layerMask;
    //public GameObject carPrefab;//小推车的预制体
    public Transform plantsTran;
    void Awake(){
        GenerateMap();
    }

    void Update(){
        
    }

    public void GenerateMap(){
        floor.localScale = deltaSize;
        floor.localPosition = Vector3.right*(deltaSize.x*.5f - .5f);
    }

    public Vector3 ToGridPos(Vector3 pos){
        Vector3 p = transform.InverseTransformPoint(pos);
        return new Vector3(Mathf.Round(p.x), Mathf.Round(p.y), 0);
    }

    public void GeneratePlant(AssetId id, Vector3 point){
        Vector3 pos = ToGridPos(point);
        Plant newPlant = Instantiate<Plant>(LocalData.instance.GetPlantArticle(id).plantPrefab, plantsTran);
        newPlant.transform.localPosition = pos;
    }
}
