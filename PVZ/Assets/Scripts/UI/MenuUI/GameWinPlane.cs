using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWinPlane : BaseUI
{
    public GameObject newCard;
    public Image wbg;
    public GameObject nextLevelPlane;
    public ArrowUI arrow;

    //卡片掉落
    public void ShowCard(Vector2 startPos, PlantAssetId plantId = PlantAssetId.None){
        newCard.GetComponent<Image>().sprite = LocalData.instance.GetPlantArticle(plantId).cardSprite;
        newCard.SetActive(true);
        arrow.Hide();
        StartCoroutine(AnimateDropCard(startPos));
    }
    //拾取卡片
    public void CollectCard(){
        arrow.Hide();
        wbg.gameObject.SetActive(true);
        //动画
        StartCoroutine(AnimateCollectCard(3));
        //音乐
        AudioManager.instance.PlaySound("GameWin");
    }
    //下一关
    public void NextLevel(){
        LocalData.instance.curLevel ++;
        SceneManager.LoadScene(LocalData.instance.curLevel);
    }

    //卡片掉落动画
    IEnumerator AnimateDropCard(Vector2 originPos){
        RectTransform card = newCard.GetComponent<RectTransform>();
        card.anchoredPosition = originPos;
        float percent = 0;
        float duration = .6f;
        Vector2 targetPos = originPos + Vector2.one * (-20);
        while(percent < 1){
            percent += Time.deltaTime / duration;
            card.anchoredPosition = Vector2.Lerp(originPos, targetPos, Mathf.Sqrt(percent));
            yield return null;
        }
        arrow.Show();
    }

    //拾取卡片时的动画
    IEnumerator AnimateCollectCard(float duration){
        RectTransform card = newCard.GetComponent<RectTransform>();
        float percent = 0;
        Vector2 originPos = card.anchoredPosition;
        Vector2 targetPos = new Vector2(512, 514);
        Color originColor = new Color(1,1,1,0);
        Vector3 originScale = card.localScale;
        while(percent < 1){
            percent += Time.deltaTime / duration;
            card.anchoredPosition = Vector2.Lerp(originPos, targetPos, percent);
            wbg.color = Color.Lerp(originColor, Color.white, percent);
            card.localScale = Vector3.Lerp(originScale, Vector3.one * 2, percent);
            yield return null;
        }

        nextLevelPlane.SetActive(true);
        wbg.gameObject.SetActive(false);
    }
}
