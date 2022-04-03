using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squash : Plant
{
    public Animator animator;
    private bool isUsed;
    protected override void DoSomething()
    {
        if(isUsed ) return;
        Collider[] colliders = Physics.OverlapBox(transform.position + Vector3.right * .22f,
                                                new Vector3(.12f, .12f, .01f),
                                                Quaternion.identity,
                                                LayerMask.GetMask("Zombie"));
        if(colliders.Length > 0){
            isUsed = true;
            animator.SetBool("attack", true);
            GetComponent<SpriteRenderer>().sortingOrder = 6;
            //transform.localPosition += Vector3.right;
            //
            AudioManager.instance.PlaySound("Squash", transform.position);
            //TODO:压瘪敌人2333
            foreach(Collider collider in colliders){
                collider.gameObject.GetComponent<Zombie>().PressDie(.5f, .5f);
            }
        }
    }
}
