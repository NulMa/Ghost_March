using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject uiNotice;

    enum Achive { UnlockOrchid, UnlockChrysanthemum, UnlockBamboo }
    Achive[] achives;
    WaitForSecondsRealtime wait; 

    private void Awake() {
        achives = (Achive[])Enum.GetValues(typeof(Achive));
        wait = new WaitForSecondsRealtime(3);

        if (!PlayerPrefs.HasKey("MyData")){
            Init();
        }
    }

    void Init() {
        PlayerPrefs.SetInt("MyData", 1);

        foreach(Achive achive in achives) {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }

    private void Start() {
        UnlockCharacter();
    }

    void UnlockCharacter() {
        for(int index = 0; index < lockCharacter.Length; index++) {
            string achiveName = achives[index].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;

            lockCharacter[index].SetActive(!isUnlock);
            unlockCharacter[index].SetActive(isUnlock);
        }
    }

    private void LateUpdate() {
        foreach(Achive achive in achives) {
            CheckAchive(achive);
        }
    }

    void CheckAchive(Achive achive) {
        bool isachive = false;

        switch (achive) {
            case Achive.UnlockOrchid:
                isachive = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
                break;
            case Achive.UnlockChrysanthemum:
                isachive = GameManager.instance.health <= 0; //결재로 변경
                break;
            case Achive.UnlockBamboo:
                //isachive = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
                break;
        }

        if(isachive && PlayerPrefs.GetInt(achive.ToString()) == 0) {
            PlayerPrefs.SetInt(achive.ToString(), 1);

            for(int index = 0; index < uiNotice.transform.childCount; index++) {
                bool isActive = index == (int)achive;
                uiNotice.transform.GetChild(index).gameObject.SetActive(isActive);
            }
            StartCoroutine(NoticeRoutine());
        }
    }

    IEnumerator NoticeRoutine() {
        uiNotice.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp, 0);

        yield return wait;
        uiNotice.SetActive(false);

    }
}
