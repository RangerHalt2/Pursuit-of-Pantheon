using UnityEngine;

[System.Serializable]
public class FollowerData
{
    public string followerID;
    public string displayName;
    public int position;
    public int classID;
    public float maxHP;
    public float currentHP;
    public float strength;
    public float magic;
    public float defense;
    public float resistance;
    public float speed;
    public float bonusMaxHP;
    public float bonusStrength;
    public float bonusMagic;
    public float bonusDefense;
    public float bonusResistance;
    public float bonusSpeed;

    // Converts a FollowerStatblock to FollowerData
    public static FollowerData FromStatblock(FollowerStatblock statblock)
    {
        return new FollowerData
        {
            followerID = statblock.followerID,
            displayName = statblock.displayName,
            position = statblock.position,
            classID = statblock.classID,
            maxHP = statblock.maxHP,
            currentHP = statblock.currentHP,
            strength = statblock.strength,
            magic = statblock.magic,
            defense = statblock.defense,
            resistance = statblock.resistance,
            speed = statblock.speed,
            bonusMaxHP = statblock.bonusMaxHP,
            bonusStrength = statblock.bonusStrength,
            bonusMagic = statblock.bonusMagic,
            bonusDefense = statblock.bonusDefense,
            bonusResistance = statblock.bonusResistance,
            bonusSpeed = statblock.bonusSpeed
        };
    }

    // Converts FollowerData to a Follower GameObject
    public GameObject ToFollowerGameObject(GameObject prefab)
    {
        GameObject go = Object.Instantiate(prefab);
        FollowerStatblock statblock = go.GetComponent<FollowerStatblock>();
        statblock.followerID = followerID;
        statblock.displayName = displayName;
        statblock.position = position;
        statblock.classID = classID;
        statblock.maxHP = maxHP;
        statblock.currentHP = currentHP;
        statblock.strength = strength;
        statblock.magic = magic;
        statblock.defense = defense;
        statblock.resistance = resistance;
        statblock.speed = speed;
        statblock.bonusMaxHP = bonusMaxHP;
        statblock.bonusStrength = bonusStrength;
        statblock.bonusMagic = bonusMagic;
        statblock.bonusDefense = bonusDefense;
        statblock.bonusResistance = bonusResistance;
        statblock.bonusSpeed = bonusSpeed;
        return go;
    }
}