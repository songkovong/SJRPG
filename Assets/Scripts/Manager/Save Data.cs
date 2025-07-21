using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    // Player Stat
    public int level;
    public float exp;
    public float expCount;
    public float maxHealth;
    public float currentHealth;
    public float maxMagic;
    public float currentMagic;
    public float magicRecoveryRate;
    public float moveSpeed;
    public float sprintSpeed;
    public float rotationSpeed;
    public float dependRate;
    public float attackDamage;
    public int weaponCode;
    public int statPoint;
    public int strength;
    public int agility;
    public int magic;
    public int depend;
    public int skillStatPoint;

    // Player Position and Rotation
    public float posX, posY, posZ;
    public float rotX, rotY, rotZ, rotW;

    // Inventory Slot
    public List<ItemSlotData> slots = new List<ItemSlotData>();

    // Coin
    public int coin;

    // Player Skill
    public List<SkillSaveData> skills = new List<SkillSaveData>();
}

[Serializable]
public class ItemSlotData
{
    public string itemName;
    public int itemCount;
}

[Serializable]
public class SkillSaveData
{
    public int code;
    public int level;
    public float cooltime;
    public float damage;
    public float cost;
    public float duration;
    public float timer;
    public float masteryStat;
    public int playerCombo;
}

