using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public Transform pauseUI;

    public void pauseOn() {
        GameManager.instance.isLive = false;
        Time.timeScale = 0;
        pauseUI.localScale = Vector3.one;
    }

    public void pauseOff() {
        GameManager.instance.isLive = true;
        Time.timeScale = 1;
        pauseUI.localScale = Vector3.zero;
    }

}
