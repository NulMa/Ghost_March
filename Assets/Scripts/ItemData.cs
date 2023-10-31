using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptble OBject/ItemData")]
public class ItemData : ScriptableObject{

    public enum ItemType { Melee, Range, Glove, Shoe, Heal, Splash}

    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    [TextArea]
    public string itemDesc;
    public Sprite itemIcon;


    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount; //melee = count, range = penatration
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")]
    public GameObject projectile;
    public Sprite hand;
}
