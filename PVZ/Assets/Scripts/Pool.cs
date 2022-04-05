using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    static Transform trans;
    static Dictionary<GameObject, List<GameObject> > dict = new Dictionary<GameObject, List<GameObject>>();
    void Start() {
        trans = GetComponent<Transform>();
        dict.Clear();
    }
    static public GameObject GetObject(GameObject prefab){
        GameObject obj = null;
        if(!dict.ContainsKey(prefab)){
            dict.Add(prefab, new List<GameObject>());
        }
        List<GameObject> list = dict[prefab];
        if(list.Count > 0){
            obj = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
        }
        else{
            obj = Instantiate<GameObject>(prefab, trans);
        }
        obj.SetActive(true);
        return obj;
    }
    static public void DestroyObject(GameObject obj, GameObject prefab){
        if(!dict.ContainsKey(prefab)){
            dict.Add(prefab, new List<GameObject>());
        }
        obj.SetActive(false);
        dict[prefab].Add(obj);
        obj.transform.SetParent(trans);
    }
}
