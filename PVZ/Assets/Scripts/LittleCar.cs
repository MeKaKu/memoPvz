using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleCar : MonoBehaviour
{
    bool isStarted;
    public Transform carTrans;
    float speed = .04f;
    private void Start() {
        StartCoroutine(ShowCar());
    }
    private void Update() {
        Collider[] colliders = 
            Physics.OverlapBox(carTrans.position, Vector3.one * .2f, Quaternion.identity, LayerMask.GetMask("Zombie"));
        if(isStarted){
            //
            foreach(Collider collider in colliders){
                Zombie zombie = collider.gameObject.GetComponent<Zombie>();
                if(!zombie.isDead){
                    zombie.CarPressDie(.4f);
                }
            }
            transform.localPosition += Vector3.right * speed;
        }
        else{
            if(colliders.Length > 0){
                StartRun();
            }
        }
    }
    public void StartRun(){
        isStarted = true;
    }

    IEnumerator ShowCar(){
        float percent = 0;
        float duration = 2;
        Vector3 targetPosition = new Vector3(0, transform.localPosition.y, 0);
        while(percent < 1){
            percent += Time.deltaTime / duration;
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, percent * percent);
            yield return null;
        }
    }
}
