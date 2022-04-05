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
    public event System.Action onStartSpawn;//开始产生僵尸事件
    public event System.Action<int> onNewWave;//每一波开始时触发
    public event System.Action onNoWave;//清除完所有波时触发
    public Transform zombiesTrans;
    int prePosY;
    int n;
    System.Random rand;
    public Vector3 lastDeadZombiePos;
    List<Zombie> allZombies = new List<Zombie>();
    int zCnt;

    private void Start() {
        rand = new System.Random((int)Time.time);
        n = Mathf.RoundToInt(FindObjectOfType<MapGenerator>().deltaSize.y*.5f-.5f);
    }

    /// <summary>
    /// 延时开始生成僵尸
    /// </summary>
    /// <param name="duration">延迟时间</param>
    public void DelayStartSpawn(float duration) {
        Invoke("StartSpawn", duration);
    }

    public void StartSpawn(){
        onStartSpawn?.Invoke();
        AudioManager.instance.PlaySound("StartSpawnZombies", Vector3.zero);
        foreach(var zombie in allZombies){
            zombie.transform.localPosition += Vector3.left * 30;
        }
        NextWave();
    }
    public void PreviewZombie(int compressPercent){
        float sum = 0;
        foreach(var wave in waves){
            sum += wave.zombies.Count;
        }
        foreach(var wave in waves){
            foreach(var zombieInfo in wave.zombies){
                Zombie zombiePrefab = LocalData.instance.GetZombiePrefab(zombieInfo);
                Zombie zombie = Instantiate<Zombie>(zombiePrefab, zombiesTrans);
                float posY = n - (2f*n*allZombies.Count/sum);
                zombie.transform.localPosition = new Vector3(Random.Range(9.5f, 12.5f), posY, - posY * .001f + .0005f);
                allZombies.Add(zombie);
            }
        }
        zCnt = 0;
    }
    private void Update() {
        if(remainToSpawner > 0 && Time.time > nextSpawnerTime){
            nextSpawnerTime = Time.time + curWave.spawnInterval;
            //生成僵尸
            int index = curWave.zombies.Count - remainToSpawner;
            
            Zombie newZombie;
            if(zCnt < allZombies.Count){
                newZombie = allZombies[zCnt];
                zCnt ++;
            }
            else{
                Zombie zombiePrefab = LocalData.instance.GetZombiePrefab(curWave.zombies[index]);
                newZombie = Instantiate<Zombie>(zombiePrefab, zombiesTrans);
            }
            int curPosY = prePosY;
            while(curPosY == prePosY){
                curPosY = rand.Next(-n, n + 1);
            }
            prePosY = curPosY;
            newZombie.transform.localPosition = new Vector3(9, curPosY, curPosY * .001f + .0005f);
            newZombie.onDeath += OnZombieDead;
            newZombie.onZombieDead += (pos)=>{
                lastDeadZombiePos = pos;
            };
            newZombie.StartMove();
            remainToSpawner --;
            //僵尸嗷嗷叫
            if(index == 0){
                AudioManager.instance.PlaySound("ZombieAoAoAo", newZombie.transform.position);
            }
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
            onNoWave?.Invoke();
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
