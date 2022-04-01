using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Player player;
    Spawner spawner;
    private void Start() {
        //
        player = FindObjectOfType<Player>();
        if(player == null){
            throw new System.Exception("No Player");
        }
        player.onDeath += GameOver;
        //
        spawner = FindObjectOfType<Spawner>();
        if(spawner == null){
            throw new System.Exception("No Spawner");
        }
        spawner.onNoWave += GameWin;
    }

    void GameOver(){
        //

    }

    void GameWin(){
        //
    }
}
