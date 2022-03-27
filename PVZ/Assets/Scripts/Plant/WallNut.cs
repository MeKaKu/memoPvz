using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNut : Plant
{
    public SpriteAnimator spriteAnimator;
    public Sprite[] percent66Sprites;
    public Sprite[] percent33Sprites;
    int curInd = -1;

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if(hp > .66f * maxHp){
        }
        else if(hp > .33f * maxHp){
            if(curInd != 0){
                curInd = 0;
                spriteAnimator.sprites = percent66Sprites;
            }
        }
        else{
            if(curInd != 1){
                curInd = 1;
                spriteAnimator.sprites = percent33Sprites;
            }
        }
    }
}
