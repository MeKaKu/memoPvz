using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : Plant
{
    public GameObject bulletPrefab;
    public Transform muzzleTrans;
    protected override void Update()
    {
        //TODO：判断是否有敌人
        if(Physics.Raycast(transform.position, Vector3.right, 4, LayerMask.GetMask("Zombie")) ){
            base.Update();
        }
    }

    protected override void DoSomething()
    {
        GameObject bulletObj = Pool.GetObject(bulletPrefab);
        bulletObj.transform.SetParent(transform);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.InitBullet();
        bullet.transform.localPosition = muzzleTrans.localPosition;
        bullet.onDestroy = ()=>{
            Pool.DestroyObject(bulletObj, bulletPrefab);
        };
        //音效
        AudioManager.instance.PlaySound("PeaShoot", transform.position + Vector3.forward*5);
        //检查子弹在产生时是否在敌人碰撞体里面
        bullet.CheckCollisionOnStart();
    }
}
