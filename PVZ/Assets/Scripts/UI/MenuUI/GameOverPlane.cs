using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPlane : BaseUI
{
    public Transform zombieWin;
    public GameObject overMenu;
    public override void Show()
    {
        base.Show();
        StartCoroutine(AnimateZombieWin());
    }

    IEnumerator AnimateZombieWin(){
        float duration = 2;

        float percent = 0;
        Vector3 originScale = Vector3.one * .1f;
        Vector3 targetScale = Vector3.one * .9f;
        while(percent < 1){
            percent += Time.deltaTime / duration;
            zombieWin.localScale = Vector3.Lerp(originScale, targetScale, percent);
            yield return null;
        }
        
        Invoke("ShowOverMenu", 2);
    }

    void ShowOverMenu(){
        AudioManager.instance.PlaySound("GameLost", Vector3.zero);
        overMenu.SetActive(true);
    }
}
