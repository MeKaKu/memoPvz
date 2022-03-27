using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Alive
{
    [Header(">移动")]
    public float moveSpeed = 5;//移动速度
    public Vector3 moveDir = Vector3.left;//移动方向
    public bool isMoving = true;//是否正在移动
    [Header(">攻击")]
    public float attackPower = 5;//攻击力
    public float attackInterval = 1;//攻击间隔
    private float nextAttackTime;//下一次攻击时间
    public LayerMask attackLayer;//攻击对象的layer
    public bool isAttacking;//是否正在攻击
    [Header(">动画")]
    public Animator animator;//动画控制器
    private float localRefactor = .01f;
    protected virtual void Update(){
        ListenAttack();
        Move();
    }
    protected virtual void Move(){
        if(isMoving){
            transform.Translate(moveDir * moveSpeed * localRefactor * Time.deltaTime, Space.Self);
        }
    }

    protected virtual void ListenAttack(){
        if(Physics.Raycast(transform.position, moveDir,out RaycastHit hit, localRefactor, attackLayer)){
            //进行攻击
            if(!isAttacking){
                animator.SetBool("attack", true);
                if(!isDead) isAttacking = true;
            }
            isMoving = false;
        }
        else{
            //结束攻击
            if(isAttacking){
                animator.SetBool("attack", false);
            }
            isAttacking = false;
            if(!isDead) isMoving = true;
        }
        if(isAttacking){
            if(Time.time > nextAttackTime){
                nextAttackTime = Time.time + attackInterval;
                Attack(hit);
            }
        }
    }
    protected virtual void Attack(RaycastHit hit){
        Plant plant = hit.collider.gameObject.GetComponent<Plant>();
        if(plant != null) plant.TakeDamage(attackPower);
    }

    /// <summary> 僵尸受到伤害
    /// 死亡有延迟，掉头一段时间后倒地
    /// </summary>
    /// <param name="damage">伤害值</param>
    public override void TakeDamage(float damage)
    {
        if(isDead ) return;
        hp -= damage;
        if(hp <= 0){
            isDead = true;
            //掉头
            animator.SetBool("lostHead", true);
            animator.SetLayerWeight(1, 1);
            animator.Play("headFall");
            //播放死亡动画
            Invoke("AnimateDie", 2);
        }
    }
    private void AnimateDie(){
        isMoving = false;
        animator.SetBool("dead", true);
    }
    public void OnCompleteDieAnim(){
        Die();
    }

    /// <summary>
    /// 被炸死
    /// </summary>
    public void BoomDie(){
        isMoving = false;
        if(isDead ) return;
        TakeDamage(hp - .1f);
        isDead = true;
        animator.Play("boomDie");
    }
}
