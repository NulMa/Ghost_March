using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Souls : MonoBehaviour
{
    public SpriteRenderer spriter;
    public Rigidbody2D target;
    public bool isHeal;
    public float speed;
    public float range;
    Rigidbody2D rigid;
    //Color colors;
    public float exp;

    public Color[] color;


    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        range = GameManager.instance.SoulRange;
        if (!GameManager.instance.isLive)
            return;

        if (Vector3.Distance(target.position, rigid.position) > range)
            return;


        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);



    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag != "Player")
            return;

        if(isHeal != true) {
            GameManager.instance.GetExp(exp);
        }
        else {
            if(GameManager.instance.health > GameManager.instance.maxHealth - 10) {
                GameManager.instance.health = GameManager.instance.maxHealth;
            }
            else {
                GameManager.instance.health = GameManager.instance.health + 10;
            }
            
        }
        gameObject.SetActive(false);
    }



}
