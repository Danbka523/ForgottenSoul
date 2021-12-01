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
    //private int maxDefense = 30;

    public bool isParalysed;
    public bool isFreezed;
    public bool isFired;
    public bool isNegatived;
   

    public bool isDead;

    public int negativeEffectRound;

    //обынчый урон
    public void DealPhysDamage(int amount, int percentBoost=0) {
        int currentDMG = amount + amount * percentBoost / 100;
        currentHp -= (currentDMG - currentDMG * defense / 100);
        if (currentHp < 0) {
            currentHp = 0;
            isDead = true;
        }
    }

    //огненный урон
    public void DealFireDamage(int amount,int curRound) {
        DealPhysDamage(amount);
        if (Random.Range(1, 101) >= 92 && !isNegatived)
        {
            isFired = true;
            isNegatived = true;
            negativeEffectRound = curRound;
        }
    }

    //ледяной урон
    public void DealIceDamage(int amount, int curRound)
    {
        DealPhysDamage(amount);
        if (Random.Range(1, 101) >= 92 && !isNegatived)
        {
            isFreezed = true;
            isNegatived = true;
            negativeEffectRound = curRound;
        }
    }

    //урон который игнорирует показатель брони
    public void DealDamage(int amout)
    {
        currentHp -= amout;
        if (currentHp < 0)
        {
            currentHp = 0;
            isDead = true;
        }
    }

    //дечение
    public void HealUnit(int amout, int percetBoost = 0) {
        currentHp += amout+amout*percetBoost/100;
        if (currentHp > maxHp)
            currentHp = maxHp;
    }


}
