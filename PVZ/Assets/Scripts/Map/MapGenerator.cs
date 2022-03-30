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
    int n, m;
    GameObject[] grids;
    void Awake(){
        n = Mathf.RoundToInt(deltaSize.x);
        m = Mathf.RoundToInt(deltaSize.y);
        grids = new GameObject[n * m];
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

    /// <summary> 种植物
    /// 在指定位置种下指定植物
    /// </summary>
    /// <param name="id">植物id</param>
    /// <param name="point">种植的位置</param>
    /// <returns>是否成功种植</returns>
    public bool GeneratePlant(PlantAssetId id, Vector3 point){
        Vector3 pos = ToGridPos(point);
        int index = GridToIndex(pos);
        if(grids[index]!=null){
            return false;
        }
        Plant newPlant = Instantiate<Plant>(LocalData.instance.GetPlantArticle(id).plantPrefab, plantsTran);
        newPlant.transform.localPosition = pos;
        grids[index] = newPlant.gameObject;
        AudioManager.instance.PlaySound("PlantPlant", point);
        return true;
    }

    public int GridToIndex(Vector3 gridPos){
        int x = Mathf.RoundToInt(gridPos.x);
        int y = Mathf.RoundToInt(gridPos.y + deltaSize.y * .5f - .5f);
        return x + y * n;
    }

    public bool ClearPlant(Vector3 point){
        Vector3 pos = ToGridPos(point);
        int index = GridToIndex(pos);
        if(grids[index] == null){
            return false;
        }
        Destroy(grids[index]);
        grids[index] = null;
        AudioManager.instance.PlaySound("ClearPlant", point);
        return true;
    }
}
