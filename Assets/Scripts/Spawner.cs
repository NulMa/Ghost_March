using System.Collections;
using System.Collections.Generic;
//using UnityEditor.U2D.Path;
using UnityEngine;

public class Spawner : MonoBehaviour{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;
    float timer;
    int level;

    private void Awake() {
        spawnPoint = GetComponentsInChildren<Transform>();
        levelTime = GameManager.instance.maxGameTime / spawnData.Length;
    }
    private void Update() {
        if (!GameManager.instance.isLive)
            return;

        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / levelTime), spawnData.Length -1);

        if(timer > spawnData[level].spawnTime) {
            timer = 0;
            Spawn();
        }
    }

    void Spawn() {

        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;

        Vector3 pos = spawnPoint[Random.Range(1, spawnPoint.Length)].position;

        //spawn type by perentage
        int per = Random.Range(0, spawnData[level].percentage.Length);

        enemy.GetComponent<Enemy>().Init(spawnData[level].mobs[spawnData[level].percentage[per]]);

        Debug.Log(spawnData[level].mobs[spawnData[level].percentage[per]].mobType);

        if(spawnData[level].mobs[spawnData[level].percentage[per]].mobType == MobData.MobType.swarm) {
            
            for (int i = 0; i < 10; i++) {

                enemy = GameManager.instance.pool.Get(0);

                int ranx = Random.Range(-5, 5);
                int rany = Random.Range(-5, 5);
                enemy.transform.position = pos - new Vector3(ranx*0.1f, rany*0.1f, 0);
                enemy.GetComponent<Enemy>().Init(spawnData[level].mobs[spawnData[level].percentage[per]]);
            }
        }
    }
}   

[System.Serializable] // 퍼블릭 참조용
public class SpawnData {

    public MobData[] mobs;
    public int[] percentage;

    public float spawnTime;
    /*
    public int spriteType;
    public int health;
    public float speed;

    public float scale;
    public Color color;
    public int mass;
    */
}