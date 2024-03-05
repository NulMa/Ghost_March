using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Specialz : MonoBehaviour{
    public RectTransform[] CommDir;
    public int CommNum;
    public bool isGetSP;
    public float Timer;

    Vector3 up = Vector3.zero;
    Vector3 right = new Vector3(0, 0, 90);
    Vector3 down = new Vector3(0, 0, 180);
    Vector3 left = new Vector3(0, 0, 270);

    private void Awake() {
        StartCoroutine(GenDir());
        CommNum = 0;
    }


    private void Update() {
        Timer += Time.deltaTime;

        if (CommNum < CommDir.Length &&CommDir[CommNum].eulerAngles.z == GameManager.instance.myDir) {
            CommDir[CommNum].gameObject.SetActive(false);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.ui, 1);
            CommNum++;

        }

        if(CommNum == 4 && isGetSP == false) {
            isGetSP = true;
            GameManager.instance.SpecialGauge += 12f;
            AudioManager.instance.PlaySfx(AudioManager.Sfx.ui, 0);
        }

    }




    public void testingFn() {

        switch (CommDir[CommNum].transform.rotation.z) {
            case  0:
                break;
            case 90:
                break;
            case 180:
                break;
            case 270:
                break;
        }
    }

    IEnumerator GenDir() { //generate command Directions
        while (true) {
            yield return new WaitForSeconds(5f);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.ui, 2);
            Timer = 0;

            CommNum = 0;
            isGetSP = false;

            for (int i = 0; i < CommDir.Length; i++) {
                int Dir = Random.Range(0, 4);
                CommDir[i].gameObject.SetActive(true);

                switch (Dir) {
                    case 0:
                        CommDir[i].eulerAngles = up;
                        break;
                    case 1:
                        CommDir[i].eulerAngles = right;
                        break;
                    case 2:
                        CommDir[i].eulerAngles = down;
                        break;
                    case 3:
                        CommDir[i].eulerAngles = left;
                        break;
                }
            }
        }
    }

}
