using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBullet : Bullet
{
    public override void ImpactObject(Alive alive, Vector3 hitPos)
    {
        Zombie zombie = alive as Zombie;
        zombie.SlowDown(.6f, 2.8f, new Color(.5f,.5f,1,1));
        base.ImpactObject(alive, hitPos);
    }
}
