using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    public Sprite[] sprites;
    public int animationRate = 25;
    float nextChangeTime;
    int index = 0;
    SpriteRenderer spriteRenderer;
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update() {
        if(animationRate == 0) return;
        if(Time.time > nextChangeTime){
            nextChangeTime = Time.time + (1f/animationRate);
            changeSprite();
        }
    }
    void changeSprite(){
        if(index >= sprites.Length){
            index = 0;
        }
        spriteRenderer.sprite = sprites[index];
        index ++;
    }
}
