using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpclMv_Anim_Evnt : MonoBehaviour
{
    // Start is called before the first frame update
    public void End() {
        GameManager.instance.EndSpclMv();
        GameManager.instance.gv.GetComponent<GV>().StartCoroutine("CaOff");
    }
}
