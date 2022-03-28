using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeZombie : Zombie
{
    [Header(">头上护具")]
    public SpriteRenderer coneSR;
    public Sprite[] coneSprites;
    private int curConeInd = 0;
    private bool isLostCone = false;
    protected override void Start() {
        base.Start();
        coneSR.sprite = coneSprites[curConeInd];
    }
    protected override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Z)){
            TakeDamage(5);
        }
    }
    public override void TakeDamage(float damage){
        if(isDead) return;
        hp -= damage;
        if(hp > .75f * maxHp){
            if(curConeInd != 0){
                curConeInd = 0;
                coneSR.sprite = coneSprites[curConeInd];
            }
        }
        else if(hp > .5f * maxHp){
            if(curConeInd != 1){
                curConeInd = 1;
                coneSR.sprite = coneSprites[curConeInd];
            }
        }
        else if(hp > .25f * maxHp){
            if(curConeInd != 2){
                curConeInd = 2;
                coneSR.sprite = coneSprites[curConeInd];
            }
        }
        else{
            if(!isLostCone){
                isLostCone = true;
                animator.SetBool("lostCone", isLostCone);
                animator.SetLayerWeight(2, 1);
                animator.Play("coneFall");
            }
        }
        hp += damage;
        base.TakeDamage(damage);
    }
}
