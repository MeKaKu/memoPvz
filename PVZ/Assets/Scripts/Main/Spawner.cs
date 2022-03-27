using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Wave[] waves;
    private Wave curWave;//当前波
    private int curWaveInd;//当前波数Index
    private float nextSpawnerTime;//下一次生成僵尸的时间
    private int remainToSpawner;//当前波剩余待生成僵尸数目
    private int remainAlive;//当前剩余存活僵尸
    public event System.Action<int> onNewWave;//每一波开始时触发
    public Transform zombiesTrans;

    private void Start() {
        NextWave();
    }
    private void Update() {
        if(remainToSpawner > 0 && Time.time > nextSpawnerTime){
            nextSpawnerTime = Time.time + curWave.spawnInterval;
            //生成僵尸
            int index = curWave.zombies.Count - remainToSpawner;
            Zombie zombiePrefab = LocalData.instance.GetZombiePrefab(curWave.zombies[index]);
            Zombie newZombie = Instantiate<Zombie>(zombiePrefab, zombiesTrans);
            newZombie.transform.localPosition = new Vector3(9, Random.Range(-2, 3), 0);
            newZombie.onDeath += OnZombieDead;
            remainToSpawner --;
        }
    }
    void NextWave(){
        if(curWaveInd < waves.Length){
            curWave = waves[curWaveInd];
            nextSpawnerTime = Time.time + curWave.delayTime;//下一波生成敌人的时间
            remainToSpawner = remainAlive = curWave.zombies.Count;
            onNewWave?.Invoke(curWaveInd + 1);
        }else{
            //TODO:游戏胜利
        }
        curWaveInd ++;
    }

    void OnZombieDead(){
        remainAlive --;
        if(remainAlive == 0){
            NextWave();
        }
    }

    [System.Serializable]
    public class Wave{
        public float delayTime;//延迟时间
        public float spawnInterval;//间隔时间
        public List<ZombieAssetId> zombies;//僵尸信息
    }
}
