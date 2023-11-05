using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mob", menuName = "Scriptble OBject/MobData")]

public class MobData : ScriptableObject {
    public enum MobType { normal, swarm, boss }

    [Header("# Main Info")]
    public MobType mobType;
    public int mobId;
    public string mobDesc;


    [Header("# Level Data")]
    public int SpriteType;
    public float health;
    public float speed;
    public float scale;
    public Color color;
    public float mass;
    public float exp;

}
