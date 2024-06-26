    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Enemy : MonoBehaviour{
    public float speed;
    public float health;
    public float maxHealth;
    public float scale;
    public float exp;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;
    public GameObject soul;
    public GameObject smallHeal;
    public GameObject hudDamageText;
    public GameObject BossHud;
    public GameObject shadow;


    public Transform hudPos;

    public int spriteType;

    public Vector2 tempDir;

    bool isLive;

    Rigidbody2D rigid;
    Collider2D coll;
    CapsuleCollider2D cColl;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;
    AchiveManager achive;


    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        cColl = GetComponent<CapsuleCollider2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }

    private void FixedUpdate() {
        if (!GameManager.instance.isLive)
            return;
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))//죽었으면 실행X
            return;


        if(spriteType != 3 ) {  //3 = swarm type
            chaseLogic(true);
        }
        else {
            if(Vector2.Distance(target.position, rigid.position) < 7) { //추적 중단 거리
                chaseLogic(false);
            }
            else {
                chaseLogic(true);
            }
        }

        //플레이어 충돌시 밀리지 않게 
        rigid.velocity = Vector2.zero;
    }

    public void chaseLogic(bool onoff) {
        



        if (onoff == true) {
            Vector2 dirVec = target.position - rigid.position;
            Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; //*************
            rigid.MovePosition(rigid.position + nextVec);
            tempDir = dirVec;
        }

        // near distance, static diraction
        else {
            Vector2 nextVec = tempDir.normalized * speed * Time.fixedDeltaTime; //*************
            rigid.MovePosition(rigid.position + nextVec);   
        }
        
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

    public void Init(MobData data) {
        this.gameObject.layer = 6;
        spriter.sortingOrder = 2;
        anim.runtimeAnimatorController = animCon[data.spriteType];
        spriteType = data.spriteType;
        speed = data.speed * (GameManager.instance.hardMode ? 1.2f : 1); ;      //--­┐
        maxHealth = data.health * (GameManager.instance.hardMode ? 1.5f : 1);   //--├-> hard mode
        health = data.health * (GameManager.instance.hardMode ? 1.5f : 1);      //--┘
        scale = data.scale;
        transform.localScale = new Vector3(scale, scale, scale);
        spriter.color = data.color;
        rigid.mass = data.mass;
        exp = data.exp;

        shadow.transform.localPosition = data.shadowPos;
        coll.offset = data.collOffset;
        cColl.size = data.collSize;


        /*
    public Vector2 shadowPos;
    public Vector2 collOffset;
    public Vector2 collSize;
        */


        if (data.mobType == MobData.MobType.bigBoss) {
            BossHud.gameObject.SetActive(true);
        }
        else {
            BossHud.gameObject.SetActive(false);
        }


        if (spriteType == 3) {
            this.gameObject.layer = 7;
            spriter.sortingOrder = 3;
        }

    }

    public void mobDead() {
        isLive = false;
        coll.enabled = false;
        rigid.simulated = false;
        spriter.sortingOrder = 1;
        anim.SetBool("Dead", true);
        GameManager.instance.kill++;
        GameManager.instance.enemyKills[spriteType]++;

        int spawnHeal = Random.Range(0, 100);

        if (spawnHeal <= 97) {
            GameObject souls = Instantiate(soul);

            souls.GetComponent<Souls>().exp = exp;

            souls.transform.position = this.transform.position;
        }
        else {
            GameObject sHeal = Instantiate(smallHeal);

            sHeal.transform.position = this.transform.position;
        }

        //GameManager.instance.GetExp();
        if (GameManager.instance.isLive)
            AudioManager.instance.PlaySfx(AudioManager.Sfx.newMobDead, spriteType);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;


        GameObject HudText = Instantiate(hudDamageText);
        HudText.transform.position = hudPos.position;
        HudText.GetComponent<DamageText>().damage = Mathf.FloorToInt(collision.GetComponent<Bullet>().damage);

        health -= collision.GetComponent<Bullet>().damage;

        StartCoroutine(knockBack());

        if(health > 0) {
            anim.SetTrigger("Hit");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit, spriteType);
        }
        else {
           mobDead();
        }

        IEnumerator knockBack() {
            yield return wait;
            Vector3 playerPos = GameManager.instance.player.transform.position;
            Vector3 dirVec = transform.position - playerPos;
            rigid.AddForce(dirVec.normalized * 0.2f, ForceMode2D.Impulse);
        }
    }
    public void Dead() {
        gameObject.SetActive(false);
    }
}
