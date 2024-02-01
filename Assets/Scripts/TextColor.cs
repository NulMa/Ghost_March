using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextColor : MonoBehaviour
{
    TextMeshPro pro;

    private void Awake() {
        pro = GetComponent<TextMeshPro>();
    }
    private void LateUpdate() {
        if(float.Parse(pro.text) <= 20) { //white
            pro.color = new Color(255, 255, 255);
        }
        else if (float.Parse(pro.text) >= 40) { // red
            pro.color = new Color(255, 0, 0);
        }
        else { // yellow
            pro.color = new Color(255, 180, 0);
        }


    }
}
