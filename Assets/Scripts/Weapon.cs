using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    public int slashAngle = 0;

    float timer;
    float RotAng;
    Player player;

    private void Awake() {
        player = GameManager.instance.player;
    }

    private void Update() {
        if (!GameManager.instance.isLive)
            return;

        switch (id) {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);

                timer += Time.deltaTime;

                if (timer > 1 - (speed * 0.001f)) {
                    timer = 0f;
                    //AudioManager.instance.PlaySfx(AudioManager.Sfx.Melee, 0);
                }
                break;

            case 1:
                timer += Time.deltaTime;

                if (timer > speed) {
                    timer = 0f;
                    Fire();
                }
                break;

            case 5:
                timer += Time.deltaTime;

                if (timer > speed) {
                    timer = 0f;
                    Splash();
                }
                break;

            case 6:
                timer += Time.deltaTime;

                if (timer > speed) {
                    timer = 0f;
                    Fire();
                }
                break;

            case 7:
                //transform.Rotate(Vector3.back * speed * Time.deltaTime);

                timer += Time.deltaTime;

                if (timer > speed) {
                    timer = 0f;
                    Slash();
                }
                break;



        }
    }
    public void LevelUp(float damage, int count) {
        this.damage = damage * Character.Damage;
        this.count += count;

        if (id == 0)
            /*
            if (레벨 값 최종이면) {
                스프라이트 전부 바꾸고 밑의 배치 함수 넘어가게
            }
            */
            Batch();



        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }



    public void Evolve() {
        switch (id) {
            case 0: //shilver bell
                //GetComponentInChildren<SpriteRenderer>().sprite = 
                //speed = 150 * Character.WeaponSpeed;
                break;
            case 1: // charm
                speed = 0.5f * Character.WeaponRate;
                break;

            case 5: // Vajra
                speed = 5f * Character.WeaponRate;
                break;

            case 6: // horse bow
                speed = 3f * Character.WeaponRate;
                break;

            case 7: //Straw cutter
                speed = 2.5f * Character.WeaponSpeed;
                Slash();
                break;

        }
    }

        public void Init(ItemData data) {
        //basic setting
        name = "Weapon" + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        //property Set
        id = data.itemId;
        damage = data.baseDamage * Character.Damage;
        count = data.baseCount + Character.Count;

        for(int index = 0; index < GameManager.instance.pool.prefabs.Length; index++) {
            if(data.projectile == GameManager.instance.pool.prefabs[index]) {
                prefabId = index;
                break;
            }
        }

        switch (id) {
            case 0: //shilver bell
                speed = 150 * Character.WeaponSpeed;
                Batch();
                break;
            case 1: // charm
                speed = 0.5f * Character.WeaponRate;
                break;

            case 5: // Vajra
                speed = 5f * Character.WeaponRate;
                break;

            case 6: // horse bow
                speed = 3f * Character.WeaponRate;
                break;

            case 7: //Straw cutter
                speed = 2.5f * Character.WeaponSpeed;
                Slash();
                break;


        }
        //hand setting
        /*

        Hand hand = player.hands[(int)data.itemType];
        hand.spriter.sprite = data.hand;
        hand.gameObject.SetActive(true);

        */

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void Batch() {
        for (int index = 0; index < count; index++) {
            Transform bullet;
            
            if(index < transform.childCount) {
                bullet = transform.GetChild(index);
                AudioManager.instance.PlaySfx(AudioManager.Sfx.newWeap, 1);
            }
            else {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }
            
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero); // -100 IS INF PER.
        }
    }

    void Slash() {
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;

        if (!bullet.gameObject.activeSelf)
            bullet.gameObject.SetActive(true);

        int CritHit = 1;
        int Crit = UnityEngine.Random.Range(0, 100);

        if(Crit < count) {
            CritHit = 2;
            bullet.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        }
        else {
            CritHit = 1;
            bullet.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }


        bullet.GetComponent<Bullet>().Init(damage * CritHit, -100, Vector3.zero); // -100 IS INF PER.

        bullet.localPosition = Vector3.zero;
        bullet.parent = transform;

        if (GameManager.instance.touchDirection != Vector2.zero) {
            RotAng = MathF.Atan2(GameManager.instance.touchDirection.y, GameManager.instance.touchDirection.x) * Mathf.Rad2Deg;
        }
        


        bullet.eulerAngles = new Vector3(0, 0, RotAng - 90);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.newWeap, 2);
    }


    void Fire() {
            
        if (!player.scanner.nearestTarget)
            return;


        Vector3 targetPos = player.scanner.nearestTarget.position; //target
        Vector3 dir = targetPos - transform.position; // target vector

        dir = dir.normalized; // normalized target vector

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);



        // arrow multply
        if (id == 6) {
            for(int i=0; i < count/2; i++) {
                Transform bulletRight = GameManager.instance.pool.Get(prefabId).transform;
                Transform bulletLeft = GameManager.instance.pool.Get(prefabId).transform;
                bulletRight.transform.parent = bullet;
                bulletLeft.transform.parent = bullet;

                bulletRight.localPosition = new Vector3(0.5f * (i + 1), 0, 0);
                bulletLeft.localPosition = new Vector3(-0.5f * (i + 1), 0, 0);

                Vector3 rotVecRight = Vector3.forward;// * 15 * (i + 1);
                Vector3 rotVecLeft = Vector3.forward;// * 15 * -(i + 1);

                bulletRight.rotation = Quaternion.FromToRotation(Vector3.up, dir);
                bulletLeft.rotation = Quaternion.FromToRotation(Vector3.up, dir);

                bulletRight.Rotate(rotVecRight);    
                bulletLeft.Rotate(rotVecLeft);

                bulletRight.GetComponent<Bullet>().Init(damage, count, dir + rotVecRight);
                bulletLeft.GetComponent<Bullet>().Init(damage, count, dir + rotVecLeft);
            }
            AudioManager.instance.PlaySfx(AudioManager.Sfx.newWeap, 3);
        }
        else {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.newWeap, 0);
        }
        
    }

    void Splash() {
        if (!player.scanner.SplashTarget)
            return;
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.parent = transform;
        bullet.position = player.scanner.SplashTarget.position;
        bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lightning, 0);

    }

}
