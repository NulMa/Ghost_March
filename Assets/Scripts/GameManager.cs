using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using Unity.VisualScripting;
//using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour{
    //정적 변수 선언 = 즉시 클래스에서 호출 가능
    public static GameManager instance;
    
    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime;

    private Touch touch;

    [Header("# Player Info")]
    public int playerId;
    public float health;
    public float maxHealth = 100;
    public float RegPerSec;
    public int level;
    public int kill;
    public float exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    public float SoulRange;
    public float MaxSpecialGauge;
    public float SpecialGauge;
    public float myDir;

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public RectTransform uiJoy;
    public Vector3 joySize;
    public GameObject SpMoveButton;
    public GameObject enemyCleaner;
    public GameObject SpecialMove;
    public Text versionInfo;

    public GameObject[] SpcMoveCutScene;
    public GameObject gv;

    public Vector2 touchStartPos;
    public Vector2 touchEndPos;
    public Vector2 touchDirection;
    public float Rad;

    private void Awake() {
        instance = this;
        Application.targetFrameRate = 60;
        StartCoroutine(RegHP());
        versionInfo.text = string.Format("v.{0}", Application.version);
    }


    private void FixedUpdate() {
        Rad = MathF.Atan2(touchDirection.y, touchDirection.x) * Mathf.Rad2Deg;

        if (touchDirection == Vector2.zero)
            myDir = 1;

        switch (Rad) {
            case float rad when (rad >= 80 && rad <= 100):
                myDir = 0;
                break;

            case float rad when (rad >= -10 && rad <= 10 && rad != 0):
                myDir = 270;
                break;

            case float rad when (rad >= -100 && rad <= -80):
                myDir = 180;
                break;

            case float rad when (rad >= 170 || rad <= -170):
                myDir = 90;
                break;
        }
    }

    IEnumerator RegHP() {
        while (true) {
            yield return new WaitForSeconds(1f);
            health = health + RegPerSec;
        }
    }

    public void LateUpdate() {
        if(health > maxHealth) {
            health = maxHealth;
        }
        if (SpecialGauge > MaxSpecialGauge) {
            SpecialGauge = MaxSpecialGauge;
        }

        if(SpecialGauge == MaxSpecialGauge) {
            SpMoveButton.SetActive(true);
        }
        else {
            SpMoveButton.SetActive(false);
        }

        if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);
            switch (touch.phase) {
                case TouchPhase.Began:
                    uiJoy.SetAsFirstSibling();
                    uiJoy.localScale = joySize;
                    uiJoy.position = touch.position;
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    uiJoy.SetAsFirstSibling();
                    touchEndPos = touch.position;
                    touchDirection = (touchEndPos - touchStartPos).normalized;

                    float stickMoveRange = 10f; // 조이스틱 스틱의 이동 가능한 범위
                    uiJoy.GetChild(0).localPosition = new Vector3(touchDirection.x * stickMoveRange, touchDirection.y * stickMoveRange, 0);
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    uiJoy.SetAsFirstSibling();
                    touchDirection = Vector2.zero;
                    //uiJoy.transform.position = new Vector3(0, 5, 0);
                    uiJoy.localScale = Vector3.zero;
                    break;
            }
        }
    }

    public void GameStart(int id) {
        playerId = id;
        health = maxHealth;

        player.gameObject.SetActive(true);
        uiLevelUp.Select(0); //시작 무기 할당 // case 문으로 선택지 변경 가능
        Resume();

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select, 0);
    }

    public void GameOver() {
        StartCoroutine(GameOverRoutine());

    }

    IEnumerator GameOverRoutine() {
        isLive = false;
        yield return new WaitForSeconds(0.8f);
        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose, 0);
    }

    public void GameVictory() {
        StartCoroutine(GameVictoryRoutine());

    }

    public void SepcialMove() {
        StartCoroutine(ActiveSpecialMove());
        
    }

    public void EndSpclMv() {
        SpcMoveCutScene[playerId].SetActive(false);
    }

    IEnumerator ActiveSpecialMove() {
        SpcMoveCutScene[playerId].SetActive(true);
        SpecialGauge = 0;
        gv.GetComponent<GV>().StartCoroutine("CaOn");

        switch (playerId) {
            case 0:
                AudioManager.instance.PlaySfx(AudioManager.Sfx.etc, 0);
                SpecialMove.GetComponent<Bullet>().damage = 50;
                for (int i = 0; i < 10; i++) {
                    SpecialMove.SetActive(true);
                    yield return new WaitForSeconds(0.2f);
                    SpecialMove.SetActive(false);
                }
                break;

            case 1:
                AudioManager.instance.PlaySfx(AudioManager.Sfx.etc, 1);
                SpecialMove.GetComponent<Bullet>().damage = 10;
                for (int i = 0; i < 15; i++) {
                    SpecialMove.SetActive(true);
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.newWeap, 2);
                    yield return new WaitForSeconds(0.1f);
                    SpecialMove.SetActive(false);
                }

                yield return new WaitForSeconds(0.5f);
                SpecialMove.SetActive(true);
                AudioManager.instance.PlaySfx(AudioManager.Sfx.newWeap, 2);
                SpecialMove.GetComponent<Bullet>().damage = 100;
                yield return new WaitForSeconds(0.1f);
                AudioManager.instance.PlaySfx(AudioManager.Sfx.newWeap, 2);
                SpecialMove.SetActive(false);

                yield return new WaitForSeconds(0.7f);
                SpecialMove.GetComponent<Bullet>().damage = 200;
                SpecialMove.SetActive(true);
                AudioManager.instance.PlaySfx(AudioManager.Sfx.specialz, 1);
                yield return new WaitForSeconds(0.1f);
                SpecialMove.SetActive(false);
                break;

            case 2:
                AudioManager.instance.PlaySfx(AudioManager.Sfx.etc, 2);
                SpecialMove.GetComponent<Bullet>().damage = 200;
                for (int i = 0; i < 3; i++) {
                    SpecialMove.SetActive(true);
                    yield return new WaitForSeconds(1f);
                    SpecialMove.SetActive(false);
                }
                break;
        }
        
    }

    IEnumerator GameVictoryRoutine() {
        isLive = false;

        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(1f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win, 0);
    }



    public void GameRetry() {
        SceneManager.LoadScene(0);
    }

    public void GameQuit() {
        Application.Quit();
    }
    private void Update() {

        if (!isLive)
            return;

        gameTime += Time.deltaTime;
        
        if(gameTime > maxGameTime) {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void GetExp(float value) {
        if (!isLive)
            return;

        exp += value;
        if(exp >= nextExp[Mathf.Min(level, nextExp.Length-1)]) {
            level++;
            exp = 0;
            uiLevelUp.Show();   
        }
    }

    public void Stop() {
        isLive = false;
        Time.timeScale = 0;
        uiJoy.localScale = Vector3.zero;
    }

    public void Resume() {
        isLive = true;
        Time.timeScale = 1;
        uiJoy.localScale = joySize;
    }

    public void PauseStop() {
        isLive = false;
        Time.timeScale = 0;
        uiJoy.localScale = Vector3.zero;


    }

    public void PauseResume() {
        isLive = true;
        Time.timeScale = 1;
        uiJoy.localScale = joySize;
    }
}
