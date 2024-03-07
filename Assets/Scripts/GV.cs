using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GV : MonoBehaviour {
    
    private Volume v;
    private ChromaticAberration ca;

    void Start(){
        v = GetComponent<Volume>();
        v.profile.TryGet(out ca);
    }

    private void Update() {
        if (ca.intensity.value < 0)
            ca.intensity.value = 0;

        if (ca.intensity.value > 1)
            ca.intensity.value = 1;
    }



    IEnumerator CaOn() {
        for(int i = 0; i < 10; i++) {
            yield return new WaitForSeconds(0.05f);
            ca.intensity.value += 0.1f;
        }
        ca.intensity.value = 1f;
    }

    IEnumerator CaOff() {
        for (int i = 0; i < 10; i++) {
            yield return new WaitForSeconds(0.05f);
            ca.intensity.value -= 0.1f;
        }
        ca.intensity.value = 0f;
    }
}
