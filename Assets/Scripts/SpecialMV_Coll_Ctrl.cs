using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMV_Coll_Ctrl : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        this.transform.position = GameManager.instance.player.transform.position;
    }
}
