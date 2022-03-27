using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 5;//伤害
    public float speed = 100;//子弹飞行速度
    public Vector3 moveDir = Vector3.right;//子弹飞行方向
    public LayerMask attackLayer;//攻击的物体层级
    public Sprite bulletSprite;
    public Sprite hitSprite;
    private float lifTime = 20;
    private bool isMoving = true;
    private float refactor = .01f;
    private float targetMoveDist = .01f;
    IEnumerator animateHitEffect;
    public System.Action onDestroy;
    SpriteRenderer sr;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

    public void InitBullet(){
        isMoving = true;
        if(animateHitEffect != null){
            StopCoroutine(animateHitEffect);
            animateHitEffect = null;
        }
        transform.localPosition = Vector3.zero;
        transform.localScale = new Vector3(1,.86f,1);
        sr.color = Color.white;
        sr.sprite = bulletSprite;
        Invoke("DestroyBullet", lifTime);
    }
    
    void Update(){
        if(isMoving){
            float moveDist = Time.deltaTime * speed * refactor;
            CheckCollision(moveDist + targetMoveDist);
            transform.Translate(moveDir * moveDist, Space.Self);
        }
    }

    public void CheckCollisionOnStart(){
        //检测子弹是否产生时就在碰撞体内
        Collider[] colliders = Physics.OverlapSphere(transform.position, .1f, attackLayer);
        if(colliders.Length > 0){
            HitObject(colliders[0], transform.position);
        }
    }

    void CheckCollision(float moveDist){
        if(Physics.Raycast(transform.position, moveDir, out RaycastHit hit, moveDist, attackLayer)){
            HitObject(hit.collider, hit.point);
        }
    }

    void HitObject(Collider collider, Vector3 hitPos){
        Debug.Log(collider.name);
        isMoving = false;
        Alive alive = collider.gameObject.GetComponent<Alive>();
        if(alive != null){
            alive.TakeHit(damage, hitPos);
        }
        CancelInvoke("DestroyBullet");
        //击中特效
        if(animateHitEffect == null){
            animateHitEffect = AnimateHitEffect();
            StartCoroutine(animateHitEffect);
        }
    }

    IEnumerator AnimateHitEffect(){
        float percent = 0;
        float duration = .35f;
        sr.sprite = hitSprite;
        transform.localScale = Vector3.zero;
        while(percent < 1){
            percent += Time.deltaTime / duration;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, percent);
            sr.color = Vector4.Lerp(Color.clear, Color.white, 4*percent*(1 - percent));
            yield return null;
        }
        animateHitEffect = null;
        DestroyBullet();
    }

    public void DestroyBullet(){
        onDestroy?.Invoke();
    }
}
