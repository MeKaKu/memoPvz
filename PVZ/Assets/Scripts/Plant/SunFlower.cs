using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : Plant
{
    protected override void DoSomething()
    {
        base.DoSomething();
        GenerateSun();
    }
    void GenerateSun(){
        SunManager sunManager = FindObjectOfType<SunManager>();
        RectTransform suns = sunManager.suns.GetComponent<RectTransform>();
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(suns, screenPos, Camera.main, out Vector2 localPos);
        localPos += Vector2.up*20;
        sunManager.SpawnSun(localPos, localPos.y - 40, 1.5f, .5f);
    }
}
