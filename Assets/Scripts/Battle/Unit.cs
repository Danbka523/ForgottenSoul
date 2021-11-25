using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLVL;

    public int damage;

    public int maxHp;
    public int currentHp;
    public int defense;

    public bool isParalysed;
    public bool isSnowed;
    public bool isFired;
    public bool isDead;
    public void DealPhysDamage(int amount) {
        currentHp -= amount;
        if (currentHp <= 0) {
            currentHp = 0;
            isDead = true;
        }
    }
}
