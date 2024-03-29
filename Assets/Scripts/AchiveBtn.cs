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
                if (GameManager.instance.enemyKills[achiveCode] > conditionVal) {
                    isActive = true;
                }
                break;
            case 1:
                if (GameManager.instance.enemyKills[achiveCode] > conditionVal) {
                    isActive = true;
                }
                break;
            case 2:
                if (GameManager.instance.enemyKills[achiveCode] > conditionVal) {
                    isActive = true;
                }
                break;
            case 3:
                if (GameManager.instance.enemyKills[achiveCode] > conditionVal) {
                    isActive = true;
                }
                break;
            case 4:
                if (GameManager.instance.enemyKills[achiveCode] > conditionVal) {
                    isActive = true;
                }
                break;
        }
    }

    public void checkAchive() {
        text.text = GameManager.instance.enemyKills[achiveCode] + " / " + conditionVal;
        if (isActive) {
            Achive.SetActive(true);
        }
    }
}
