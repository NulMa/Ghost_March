using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour{
    public bool isLeft;
    public SpriteRenderer spriter;

    Vector3 rightPos = new Vector3(0.36f, -0.65f, 0);
    Vector3 rightPosReverse = new Vector3(-0.42f, -0.15f, 0);
    Quaternion leftRot = Quaternion.Euler(0, 0, -35);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135);

    SpriteRenderer player;

    private void Awake() {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    private void LateUpdate() {
        bool isReverse = player.flipX;

        if (isLeft) { //melee
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
        else { // range
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;
        }
    }
}
