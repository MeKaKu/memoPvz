using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Alive
{
    public float interval;
    float nextTime;
    protected override void Start() {
        base.Start();
        nextTime = interval + Time.time;
    }
    protected virtual void Update() {
        if(Time.time > nextTime){
            nextTime  = Time.time + interval;
            DoSomething();
        }
    }
    protected virtual void DoSomething(){
        //Debug.Log("DoSomething");
    }
}
