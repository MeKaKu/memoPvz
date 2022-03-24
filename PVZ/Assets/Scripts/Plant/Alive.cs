using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alive : MonoBehaviour
{
    public float maxHp;
    public float hp{get;private set;}
    protected bool isDead;

    public event System.Action onDeath;

    protected virtual void Start(){
        hp = maxHp;
    }
    public virtual void TakeHit(float damage, Vector3 hitPoint){
        TakeDamage(damage);
    }
    public virtual void TakeDamage(float damage){

    }
    public virtual void Die(){
        isDead = true;
        onDeath?.Invoke();
        Destroy(gameObject);
    }
}
