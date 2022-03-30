using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketZombie : ConeZombie
{
    public override void TakeHit(float damage, Vector3 hitPoint)
    {
        if(hp > .25f * maxHp){
            AudioManager.instance.PlaySound("ImpactBucketZombie", hitPoint);
        }
        else{
            AudioManager.instance.PlaySound("ImpactBaseZombie", hitPoint);
        }
        base.TakeHit(damage, hitPoint);
    }
}
