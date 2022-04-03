using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleZombie : BaseZombie
{
    bool jump;
    protected override void Update()
    {
        base.Update();
    }
    protected override void Attack(Collider hit)
    {
        if(!jump){
            if(isDead ) return;
            animator.SetBool("jump", true);
        }
        else base.Attack(hit);
    }

    void JumpCompleted(){
        jump = true;
    }
}
