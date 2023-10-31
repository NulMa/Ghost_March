using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour{
    RectTransform rect;
    Item[] items;

    private void Awake() {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show() {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp, 0);
        AudioManager.instance.EffectBgm(true);
    }

    public void Hide() {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select, 0);
        AudioManager.instance.EffectBgm(false);
    }
    
    public void Select(int index) {
        items[index].onClick();
    }

    void Next() {
        //1. disable all items
        foreach (Item item in items)
            item.gameObject.SetActive(false);

        //2. select random 3 items and enable
        int[] ran = new int[3];
        while (true) {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);
            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[2] != ran[0])
                break;
        }

        for(int index = 0; index < ran.Length; index++) {
            Item ranItem = items[ran[index]];
            if(ranItem.level == ranItem.data.damages.Length) {
                items[4].gameObject.SetActive(true);
            }
            else {
                ranItem.gameObject.SetActive(true);
            }
            
        }

        //3. max level item => consumable items
    }
}
