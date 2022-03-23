using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDMask : MonoBehaviour
{
    #region 字段
    RectTransform cdRect;//RectTransform
    RectTransform fRect;//父RectTransform
    float percent;//cd进度百分比
    float cd;//cd时长(s)
    public bool isCD{get;private set;}//是否处于cd中
    Vector2 origin;//rect起始位置
    Vector2 target;//rect移动目标位置
    #endregion

    #region 事件
    public event System.Action onStartCD;//开始CD触发事件
    public event System.Action onEndCD;//结束CD触发事件
    #endregion

    /// <summary>
    /// 开始进入CD
    /// </summary>
    /// <param name="_cd"> cd时长(s) </param>
    public void StartCD(float _cd){
        cd = _cd;
        isCD = true;
        percent = 0;
        if(cd == 0){
            percent = 2;
        }
        onStartCD?.Invoke();
    }

    void ListenCD(){
        if(isCD){
            cdRect.sizeDelta = Vector2.Lerp(origin , target, percent);
            if(percent >= 1){
                isCD = false;
                onEndCD?.Invoke();
            }
            else{
                percent += Time.deltaTime / cd;
            }
        }
    }

    #region Unity回调
    void Update() {
        ListenCD();
    }
    void Start() {
        cdRect = transform.GetComponent<RectTransform>();
        fRect = transform.parent.GetComponent<RectTransform>();
        origin = new Vector2(0, fRect.sizeDelta.y);
        target = Vector2.zero;
        StartCD(0);
    }
    #endregion
}
