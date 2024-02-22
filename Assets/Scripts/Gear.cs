using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data) {
        //basic Setting
        name = "Gear" + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        //property Setting
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }
    public void LevelUp(float rate) {
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear() {
        switch (type) {
            case ItemData.ItemType.Glove:
                Debug.Log("APPG");
                RateUp();
                break;

            case ItemData.ItemType.Shoe:
                Debug.Log("APPG");
                SpeedUp();
                break;

            case ItemData.ItemType.FoldingFan:
                Debug.Log("APPG");
                RangeUp();//range is soul absorb range
                break;

            case ItemData.ItemType.Rice:
                Debug.Log("APPG");
                RegUp();
                break;
        }
    }

    void RateUp() {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach(Weapon weapon in weapons) {
            switch (weapon.id) {
                case 0:
                    float speed = 150 * Character.WeaponSpeed;
                    weapon.speed = speed + (speed * rate);
                    break;

                case 1:
                    speed = 0.5f * Character.WeaponRate;
                    weapon.speed = speed * (1f - rate);
                    break;

                case 5:
                    speed = 5f * Character.WeaponRate;
                    weapon.speed = speed * (1f - rate);
                    break;

                case 6:
                    speed = 3f * Character.WeaponRate;
                    weapon.speed = speed * (1f - rate);
                    break;

                case 7:
                    speed = 2.5f * Character.WeaponRate;
                    weapon.speed = speed * (1f - rate);
                    break;


            }
        }
    }
    void SpeedUp() {
        float speed = 1.5f * Character.Speed;
        GameManager.instance.player.speed = speed + speed * rate;
    }

    void RangeUp() {
        Debug.Log("Range");
        GameManager.instance.SoulRange = GameManager.instance.SoulRange + GameManager.instance.SoulRange * rate;
    }

    void RegUp() {
        Debug.Log("Rate");
        GameManager.instance.RegPerSec = rate;
    }
}





