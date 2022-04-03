using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinPlane : BaseUI
{
    public void NextLevel(){
        LocalData.instance.curLevel ++;
        SceneManager.LoadScene(LocalData.instance.curLevel);
    }
}
