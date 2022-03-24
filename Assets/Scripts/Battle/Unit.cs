using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public string unitName;
    public int unitLVL;

    public int damage;
    public int weaponDamage;

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


    public Inventory inventory;

    //обынчый урон
    public void DealPhysDamage(int amount, out int dmg, int percentBoost = 0) {
        int currentDMG = (amount + amount * percentBoost / 100+Random.Range(-5, 6));
        currentDMG -= currentDMG * defense / 100;
        dmg=currentDMG;
        currentHp -= (currentDMG);
        if (currentHp < 0) {
            currentHp = 0;
            isDead = true;
        }
    }

    //огненный урон
    public void DealFireDamage(int amount,int curRound, out int dmg) {
        DealPhysDamage(amount,out dmg);
        if (Random.Range(1, 101) >= 92 && !isNegatived)
        {
            isFired = true;
            isNegatived = true;
            negativeEffectRound = curRound;
        }
    }

    //ледяной урон
    public void DealIceDamage(int amount, int curRound, out int dmg)
    {
        DealPhysDamage(amount,out dmg);
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

    public void DoAction(string name) {
       Item item=inventory.itemDB.GetItem(name);
        switch (item.id)
        {
            case 0:
                HealUnit(item.stats["Heal"]);
                inventory.RemoveItem(0);
                Debug.Log("Unit heald");
                break;
           case  1:
                break;
           case  2:
                break;
            default:
                break;
        }
    }
}
