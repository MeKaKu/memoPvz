using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryBomb : Plant
{
    protected override void Start() {
        base.Start();
        Invoke("DestroyMe", 1.7f);
    }

    void DestroyMe(){
        Destroy(gameObject);
    }
}
