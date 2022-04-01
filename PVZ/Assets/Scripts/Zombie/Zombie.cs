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
    private Collider col;
    protected override void Start() {
        base.Start();
        animator.speed = moveSpeed;
        col = GetComponent<Collider>();
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
                //音效
                if(!isDead) AudioManager.instance.PlaySound("ZombieChomp", transform.position);
            }
        }
    }
    protected virtual void Attack(Collider hit){
        Alive plant = hit.gameObject.GetComponent<Plant>();
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

    public void PressDie(float _delay, float _duration){
        isMoving = false;
        if(isDead) return;
        //TakeDamage(hp - .1f);
        isDead = true;
        StartCoroutine(AnimatePressDie(_delay, _duration));
    }

    IEnumerator AnimatePressDie(float delay, float duration){
        float percent = 0;
        while(percent < 1){
            percent += Time.deltaTime / delay;
            yield return null;
        }
        percent = 0;
        Vector3 originScale = transform.localScale;
        Vector3 originPosition = transform.localPosition;
        Vector3 targetScale = new Vector3(transform.localScale.x, 0.1f, 1);
        Vector3 targetPosition = transform.localPosition - Vector3.up * .25f;
        while(percent < 1){
            percent += Time.deltaTime / duration;
            transform.localScale = Vector3.Lerp(originScale, targetScale, percent * percent);
            transform.localPosition = Vector3.Lerp(originPosition, targetPosition, percent * percent);
            yield return null;
        }
        Die();
    }

    public override void TakeHit(float damage, Vector3 hitPoint){
        AudioManager.instance.PlaySound("ImpactBaseZombie", hitPoint);
        base.TakeHit(damage, hitPoint);
    }

    public void CarPressDie(float duration){
        isMoving = false;
        if(isDead) return;
        //TakeDamage(hp - .1f);
        isDead = true;
        StartCoroutine(AnimateCarPressDie(duration));
    }

    IEnumerator AnimateCarPressDie(float duration){
        float percent = 0;
        animator.speed = 0;
        Vector3 targetScale = new Vector3(1, 0, 1);
        Vector3 targetPosition = transform.localPosition - Vector3.up * .5f;
        Vector3 targetRotation = new Vector3(0, 0, 90);
        while(percent < 1){
            percent += Time.deltaTime / duration;
            //
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, percent);
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, targetRotation, percent);
            //
            transform.localScale = Vector3.Lerp(Vector3.one, targetScale, percent);
            yield return null;
        }
        Die();
    }
}
