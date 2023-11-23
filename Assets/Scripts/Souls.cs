using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Souls : MonoBehaviour
{

    public Rigidbody2D target;
    public float speed;
    Rigidbody2D rigid;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if (!GameManager.instance.isLive)
            return;

        if (Vector3.Distance(target.position, rigid.position) > 2)
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag != "Player")
            return;

        GameManager.instance.GetExp();
        gameObject.SetActive(false);
    }



}
