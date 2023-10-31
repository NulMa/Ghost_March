using System.Collections;
using System.Collections.Generic;
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
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[System.Serializable] // 퍼블릭 참조용
public class SpawnData {
    public float spawnTime;

    public int spriteType;
    public int health;
    public float speed;

    public float scale;
    public Color color;
    public int mass;
}