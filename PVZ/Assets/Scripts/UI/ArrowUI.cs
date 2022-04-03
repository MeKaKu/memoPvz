using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowUI : BaseUI
{
    public Vector2 direction = Vector2.down;
    public float distance = 15;
    public float duration = .5f;
    public AnimationCurve curve;
    float percent;
    RectTransform rect;
    Vector2 originPos;

    private void Start() {
        rect = GetComponent<RectTransform>();
        originPos = rect.anchoredPosition;
    }

    private void Update() {
        if(percent < 1){
            rect.anchoredPosition = originPos + curve.Evaluate(percent) * direction * distance;
            percent += Time.deltaTime / duration;
        }
        else{
            percent = 0;
        }
    }

}
