using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoMine : Plant
{
    public float readyTime = 8;
    public  Animator animator;
    private bool isUsed;

    protected override void Start()
    {
        interval = readyTime;
        base.Start();
        Invoke("DoReady", readyTime);
    }
    void DoReady(){
        animator.SetBool("ready", true);
        interval = 0;
        GetComponent<Collider>().enabled = false;
    }
    protected override void DoSomething()
    {
        if(isUsed) return;
        Collider[] colliders = Physics.OverlapBox(transform.position,
                                                new Vector3(.09f, .09f, .01f),
                                                Quaternion.identity,
                                                LayerMask.GetMask("Zombie"));
        if(colliders.Length > 0){
            isUsed = true;
            //Bomb
            animator.SetBool("bomb", true);
            GetComponent<SpriteRenderer>().sortingOrder = 6;
            foreach(Collider collider in colliders){
                collider.gameObject.GetComponent<Zombie>().Die();
            }
            //音效
            AudioManager.instance.PlaySound("PotatoMine", transform.position);
        }
    }

    void DestroyMe(){
        Destroy(gameObject);
    }
}
