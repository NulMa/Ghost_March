using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AchiveBtn : MonoBehaviour{
    Button button;
    public Text text;
    public GameObject Achive;
    public int achiveCode;
    public bool isActive;
    public int conditionVal;



    private void Awake() {
        button = GetComponent<Button>();
        
    }

    // Update is called once per frame
    void Update(){
        if (isActive) {
            transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1);
        }
        else {
            transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0.8f);
        }



        switch (achiveCode) {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
                if (GameManager.instance.enemyKills[achiveCode] > conditionVal) {
                    isActive = true;
                }
                break;

            case 100:
            case 101:
            case 102:
            case 103:
            case 104:
            case 105:
            case 106:
            case 107:
            case 108:
                if(PlayerPrefs.GetInt("Achive No." + achiveCode) == 1) {
                    isActive = true;
                }
                break;
        }
    }

    public void checkAchive() {
        if (achiveCode < 99)
            text.text = GameManager.instance.enemyKills[achiveCode] + " / " + conditionVal;
        else
            text.text = "해금 조건:\n최종 강화 완료";
        if (isActive) {
            Achive.SetActive(true);
        }
    }
}
