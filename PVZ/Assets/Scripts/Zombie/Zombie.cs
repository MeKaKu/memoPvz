using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Alive
{
    [Header(">移动")]
    public float moveSpeed = 1;//移动速度
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
    protected override void Start() {
        base.Start();
        animator.speed = moveSpeed;
    }
    protected virtual void Update(){
        ListenAttack();
        Move();
    }
    protected virtual void Move(){
        if(isMoving){
            //transform.Translate(moveDir * moveSpeed * localRefactor * Time.deltaTime, Space.Self);
        }
    }

    protected virtual void ListenAttack(){
        Collider[] colliders = Physics.OverlapBox(transform.position + moveDir * localRefactor, Vector3.one * localRefactor / 2, Quaternion.identity, attackLayer);
        if(colliders.Length > 0){
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
                Attack(colliders[0]);
            }
        }
    }
    protected virtual void Attack(Collider hit){
        Plant plant = hit.gameObject.GetComponent<Plant>();
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
