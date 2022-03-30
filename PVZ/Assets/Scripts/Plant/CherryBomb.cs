using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryBomb : Plant
{
    protected override void Start() {
        base.Start();
    }

    protected override void Update()
    {
        
    }

    protected override void DoSomething()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 6;
        Collider[] colliders = Physics.OverlapBox(transform.position,
                                                new Vector3(.4f, .4f, .1f),
                                                Quaternion.identity,
                                                LayerMask.GetMask("Zombie"));
        foreach(Collider collider in colliders){
            collider.gameObject.GetComponent<Zombie>().BoomDie();
        }
        //音效
        AudioManager.instance.PlaySound("CherryBomb", transform.position);
    }

    void DestroyMe(){
        Destroy(gameObject);
    }
}
