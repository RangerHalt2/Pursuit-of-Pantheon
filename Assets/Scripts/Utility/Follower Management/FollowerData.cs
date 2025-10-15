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
    public float vigor;
    public float power;
    public float magick;
    public float resilience;
    public float faith;
    public float agility;
    public float bonusMaxHP;
    public float bonusPower;
    public float bonusMagick;
    public float bonusResilience;
    public float bonusFaith;
    public float bonusAgility;

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
            power = statblock.power,
            magick = statblock.magick,
            resilience = statblock.resilience,
            faith = statblock.faith,
            agility = statblock.agility,
            bonusMaxHP = statblock.bonusMaxHP,
            bonusPower = statblock.bonusPower,
            bonusMagick = statblock.bonusMagick,
            bonusResilience = statblock.bonusResilience,
            bonusFaith = statblock.bonusFaith,
            bonusAgility = statblock.bonusAgility
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
        statblock.power = power;
        statblock.magick = magick;
        statblock.resilience = resilience;
        statblock.faith = faith;
        statblock.agility = agility;
        statblock.bonusMaxHP = bonusMaxHP;
        statblock.bonusPower = bonusPower;
        statblock.bonusMagick = bonusResilience;
        statblock.bonusResilience = bonusResilience;
        statblock.bonusFaith = bonusFaith;
        statblock.bonusAgility = bonusAgility;
        return go;
    }
}