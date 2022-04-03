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
            if(!animator.GetBool("jump")) AudioManager.instance.PlaySound("PoleZombie", transform.position);
            animator.SetBool("jump", true);
            col.enabled = false;
        }
        else base.Attack(hit);
    }

    void JumpCompleted(){
        jump = true;
        col.enabled = true;
    }
}
