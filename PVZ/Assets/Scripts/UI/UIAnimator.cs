using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIAnimator : MonoBehaviour
{
    public Sprite[] sprites;
    public int animationRate = 25;
    float nextChangeTime;
    int index = 0;
    int maxindex = 1;
    Image spriteRenderer;
    void Start() {
        spriteRenderer = GetComponent<Image>();
        maxindex = sprites.Length;
        if(maxindex <= 1){
            throw new System.Exception("Not animation");
        }
    }

    void Update() {
        if(animationRate == 0) return;
        if(Time.time > nextChangeTime){
            nextChangeTime = Time.time + (1f/animationRate);
            changeSprite();
        }
    }

    void changeSprite(){
        if(index >= maxindex){
            index -= maxindex;
        }
        spriteRenderer.sprite = sprites[index];
        index ++;
    }
}
