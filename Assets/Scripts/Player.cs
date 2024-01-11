using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour{
    public Vector2 inputVec;
    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] animCon;
    public bool isHit = false;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;

    public float speed;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    private void OnEnable() {
        speed *= Character.Speed;
        anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    }

    void FixedUpdate() {
        if (!GameManager.instance.isLive)
            return;
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void OnMove(InputValue value) {
        inputVec = value.Get<Vector2>();
    }

    private void LateUpdate() {
        if (!GameManager.instance.isLive)
            return;
        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0) {
            spriter.flipX = inputVec.x < 0;
        }
    }


    public IEnumerator PlayerDamaged() {
        isHit = true;
        spriter.color = new Color(255, 0, 0, 0.6f);
        yield return new WaitForSeconds(0.2f);
        spriter.color = new Color(255, 255, 255, 1);
        isHit = false;  
    }



    private void OnCollisionStay2D(Collision2D collision) {
        if (!GameManager.instance.isLive)
            return;

        GameManager.instance.health -= Time.deltaTime * 10f;
        if (isHit == false) {
            StartCoroutine(PlayerDamaged());
        }
        

        if(GameManager.instance.health < 0) {
            for(int index = 2; index < transform.childCount; index++) {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            anim.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }

    }
}
