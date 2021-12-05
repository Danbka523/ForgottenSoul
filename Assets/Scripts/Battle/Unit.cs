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

    //������� ����
    public void DealPhysDamage(int amount, out int dmg, int percentBoost = 0) {
        int currentDMG = amount + amount * percentBoost / 100+Random.Range(-5, 6);
        dmg=currentDMG;
        currentHp -= (currentDMG - currentDMG * defense / 100);
        if (currentHp < 0) {
            currentHp = 0;
            isDead = true;
        }
    }

    //�������� ����
    public void DealFireDamage(int amount,int curRound, out int dmg) {
        DealPhysDamage(amount,out dmg);
        if (Random.Range(1, 101) >= 92 && !isNegatived)
        {
            isFired = true;
            isNegatived = true;
            negativeEffectRound = curRound;
        }
    }

    //������� ����
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

    //���� ������� ���������� ���������� �����
    public void DealDamage(int amout)
    {
        currentHp -= amout;
        if (currentHp < 0)
        {
            currentHp = 0;
            isDead = true;
        }
    }

    //�������
    public void HealUnit(int amout, int percetBoost = 0) {
        currentHp += amout+amout*percetBoost/100;
        if (currentHp > maxHp)
            currentHp = maxHp;
    }


}
