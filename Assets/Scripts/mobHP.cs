using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mobHP : MonoBehaviour{

    RectTransform rect;
    Slider mobSlider;

    private void Awake() {
        rect = GetComponent<RectTransform>();
        mobSlider = GetComponent<Slider>();
    }

    void FixedUpdate(){
        this.rect.position = GetComponentInParent<RectTransform>().position;
    }
    private void LateUpdate() {
        float curHealth = GetComponentInParent<Enemy>().health;
        float maxHealth = GetComponentInParent<Enemy>().maxHealth;
        
        mobSlider.value = curHealth / maxHealth;

        Debug.Log("slider : " + mobSlider.value);
        Debug.Log("cur : " + curHealth + "//   max : " + maxHealth);
    }


}
