using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    public float speed;
    public float health;
    public float maxHealth;
    public float scale;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    public int spriteType;

    bool isLive;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }

    private void FixedUpdate() {
        if (!GameManager.instance.isLive)
            return;
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))//죽었으면 실행X
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; //*************
        rigid.MovePosition(rigid.position + nextVec);
        
        //플레이어 충돌시 밀리지 않게 
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate() {
        if (!GameManager.instance.isLive)
            return;
        if (!isLive)//죽었으면 실행X
            return;
        spriter.flipX = target.position.x < rigid.position.x;
    }

    private void OnEnable() {
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);

        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;

    }

    public void Init(SpawnData data) {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        spriteType = data.spriteType;
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
        scale = data.scale;
        transform.localScale = new Vector3(scale, scale, scale);
        spriter.color = data.color;
        rigid.mass = data.mass;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(knockBack());

        if(health > 0) {
            anim.SetTrigger("Hit");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit, spriteType);
        }
        else {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
            if(GameManager.instance.isLive)
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead, 0);
        }

        IEnumerator knockBack() {
            yield return wait;
            Vector3 playerPos = GameManager.instance.player.transform.position;
            Vector3 dirVec = transform.position - playerPos;
            rigid.AddForce(dirVec.normalized * 0.5f, ForceMode2D.Impulse);
        }
    }
    public void Dead() {
        gameObject.SetActive(false);
    }
}
