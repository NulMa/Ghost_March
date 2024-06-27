using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour{
    public ItemData data;
    public int level;
    public Weapon weapon;
    public Gear gear;
    public bool isEvo;

    public GameObject origin;

    Image icon;
    Text textLevel;
    Text textName;
    Text textDesc;
    Button btn;

    private void Awake() {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        if(!isEvo)
            textDesc = texts[2];

        textName.text = data.itemName;

        btn = GetComponent<Button>();


    }

    private void OnEnable() {
        if (origin != null) {
            level = origin.GetComponent<Item>().level;
        }



        if (isEvo == true) {
            if (level == data.damages.Length - 1) {
                transform.localScale = Vector3.zero;
            }
            else {
                transform.localScale = Vector3.one;
            }
        }




        textLevel.text = "Lv." + (level + 1);

        switch (data.itemType) {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100, data.counts[level]);
                break;

            case ItemData.ItemType.Splash:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100);
                break;

            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
            case ItemData.ItemType.Fluorite:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100);
                break;

            case ItemData.ItemType.Rice:
                textDesc.text = string.Format(data.itemDesc, data.damages[level]);
                break;

            default:
                textDesc.text = string.Format(data.itemDesc);
                break;
        }
    }


    public void onClick() {
        switch (data.itemType) {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
            case ItemData.ItemType.Splash:
                if(level == data.damages.Length - 1) { //evolve
                    transform.localScale = Vector3.zero;
                    Debug.Log("sprite change");
                    data.GetComponent<SpriteRenderer>().sprite = data.evoItem;
                    //data.GetComponent<Animator>().
                    Debug.Log("speed rate up");
                }

                if(level == 0) {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                else {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }
                level++;
                break;

            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
            case ItemData.ItemType.Fluorite:
            case ItemData.ItemType.Rice:
                if (level == 0) {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                level++;
                break;

            case ItemData.ItemType.Heal:
                GameManager.instance.health = GameManager.instance.maxHealth;
                break;
        }
        



        //achivement
        if(level == data.damages.Length) {
            GetComponent<Button>().interactable = false;
            switch (data.itemId) {
                case 0:// 은방울
                    PlayerPrefs.SetInt("Achive No." + 100, 1);
                    break;
                case 1: // 부적
                    PlayerPrefs.SetInt("Achive No." + 101, 1);
                    break;
                case 2: // 장도
                    PlayerPrefs.SetInt("Achive No." + 105, 1);
                    break;
                case 3: // 짚신
                    PlayerPrefs.SetInt("Achive No." + 106, 1);
                    break;
                //case 4: // 환약
                case 5: // 금강저
                    PlayerPrefs.SetInt("Achive No." + 102, 1);
                    break;
                case 6: // 각궁
                    PlayerPrefs.SetInt("Achive No." + 103, 1);
                    break;
                case 7: // 작두
                    PlayerPrefs.SetInt("Achive No." + 104, 1);
                    break;
                case 8: // 야명주
                    PlayerPrefs.SetInt("Achive No." + 107, 1);
                    break;
                case 9: // 생쌀
                    PlayerPrefs.SetInt("Achive No." + 108, 1);
                    break;
            }
        }
    }
}
