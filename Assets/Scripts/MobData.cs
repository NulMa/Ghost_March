using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mob", menuName = "Scriptble OBject/MobData")]

public class MobData : ScriptableObject {
    public enum MobType { normal, swarm, boss }

    [Header("# Main Info")]
    public MobType mobType;
    public Color color;


    [Header("# Level Data")]
    public int spriteType;
    public float health;
    public float speed;
    public float scale;
    public float mass;
    public float exp;

}
