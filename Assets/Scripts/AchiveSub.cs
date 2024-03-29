using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchiveSub : MonoBehaviour{

    public GameObject name;
    public GameObject image;
    public GameObject sub;

    private void Awake() {

    }

    public void ChangeSub(string UIname, Sprite sprite, string UIsub) {
        name.GetComponent<Text>().text = UIname;
        image.GetComponent<Image>().sprite = sprite;
        sub.GetComponent<Text>().text = UIsub;
    }


}
