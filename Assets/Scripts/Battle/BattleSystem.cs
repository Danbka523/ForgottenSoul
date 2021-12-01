using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOSE };

public class BattleSystem : MonoBehaviour
{   //UI
    //public Button physAttack;
    public TextMeshProUGUI historyDialog;

    //Objects
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    //Units
    Unit playerUnit;
    Unit enemyUnit;

    //BattleState
    public BattleState state;
    public int roundCounter;


    void Start()
    {
        state = BattleState.START;
        playerUnit = playerPrefab.GetComponent<Unit>();
        enemyUnit = enemyPrefab.GetComponent<Unit>();
        BattleStart();
    }


    private void BattleStart()
    {
        roundCounter = 0;
        state = BattleState.PLAYERTURN;
        UnitTurn();
    }

    public void UnitTurn()
    {
        CheckFire();
        CheckFreeze();
        if (state == BattleState.PLAYERTURN)
            roundCounter++;
        if (playerUnit.currentHp == 0)
        {
            state = BattleState.LOSE;
            EndBattle();
        }
        if (enemyUnit.currentHp == 0)
        {
            state = BattleState.WIN;
            EndBattle();
        }
        if (state == BattleState.ENEMYTURN)
            StartCoroutine(EnemyTurn());
    }

    //������� �����
    public void PhysAttack()
    {
        Actions action = Actions.DAMAGE;
        if (state == BattleState.PLAYERTURN)
        {
            enemyUnit.DealPhysDamage(playerUnit.damage);
            StartCoroutine(ChangeDialog(action, playerUnit.name, enemyUnit.name, playerUnit.damage));
            state = BattleState.ENEMYTURN;
            UnitTurn();
        }
        else if (state == BattleState.ENEMYTURN)
        {
            playerUnit.DealPhysDamage(enemyUnit.damage);
            StartCoroutine(ChangeDialog(action, enemyUnit.name, playerUnit.name, enemyUnit.damage));
            state = BattleState.PLAYERTURN;
            UnitTurn();
        }
        else
            return;
    }
    //�������� ����� (wip)
    //fire
    //========================//
    public void FireAttack()
    {
        Actions action = Actions.DAMAGE;
        if (state == BattleState.PLAYERTURN)
        {
            enemyUnit.DealFireDamage(playerUnit.damage, roundCounter);
            StartCoroutine(ChangeDialog(action, playerUnit.name, enemyUnit.name, playerUnit.damage));
            state = BattleState.ENEMYTURN;
            UnitTurn();
        }
        else if (state == BattleState.ENEMYTURN)
        {
            playerUnit.DealFireDamage(enemyUnit.damage, roundCounter);
            StartCoroutine(ChangeDialog(action, enemyUnit.name, playerUnit.name, playerUnit.damage));
            state = BattleState.PLAYERTURN;
            UnitTurn();
        }
        else
            return;
    }

    private void CheckFire()
    {
        //�������� ������
        if (playerUnit.isFired)
        {
            playerUnit.DealDamage(8);
            ChangeDialog(Actions.FIRE, "", playerUnit.name);
            if (roundCounter - playerUnit.negativeEffectRound == 3)
            {
                playerUnit.isFired = false;
                playerUnit.isNegatived = false;
            }
        }
        //�������� ����������
        if (enemyUnit.isFired)
        {
            ChangeDialog(Actions.FIRE, "", enemyUnit.name);
            enemyUnit.DealDamage(8);
            if (roundCounter - enemyUnit.negativeEffectRound == 3)
            {
                enemyUnit.isFired = false;
                enemyUnit.isNegatived = false;
            }
        }
    }
    //fire
    //========================//


    //������� �����(wip)
    //ice
    //========================//
    public void IceAttack()
    {
        Actions action = Actions.DAMAGE;
        if (state == BattleState.PLAYERTURN)
        {
            enemyUnit.DealIceDamage(playerUnit.damage, roundCounter);
            StartCoroutine(ChangeDialog(action, playerUnit.name, enemyUnit.name, playerUnit.damage));
            state = BattleState.ENEMYTURN;
            UnitTurn();
        }
        else if (state == BattleState.ENEMYTURN)
        {
            playerUnit.DealIceDamage(enemyUnit.damage, roundCounter);
            StartCoroutine(ChangeDialog(action, enemyUnit.name, playerUnit.name, playerUnit.damage));
            state = BattleState.PLAYERTURN;
            UnitTurn();
        }
        else
            return;
    }

    private void CheckFreeze()
    {
        //�������� ������
        if (playerUnit.isFreezed && state == BattleState.PLAYERTURN)
        {
            state = BattleState.ENEMYTURN;
            if (roundCounter - playerUnit.negativeEffectRound == 3)
            {
                playerUnit.isFreezed = false;
                playerUnit.isNegatived = false;
            }
        }
        //�������� ����������
        if (enemyUnit.isFreezed && state == BattleState.ENEMYTURN)
        {
            state = BattleState.PLAYERTURN;
            if (roundCounter - enemyUnit.negativeEffectRound == 3)
            {
                enemyUnit.isFreezed = false;
                enemyUnit.isNegatived = false;
            }
        }
    }

    //ice
    //========================//



    //��� ���������� (wip)
    //enemy
    //========================//
    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(2.0f);
        PhysAttack();
    }

    //enemy
    //========================//


    private void EndBattle()
    {
        ChangeDialog();

    }
    IEnumerator ChangeDialog(Actions action, string from = "", string to = "", int hp = 0)
    {
        string str = "";
        switch (action)
        {
            case Actions.DAMAGE:
                str = $"{from} damaged {to} on {hp} hp\n";
                break;
            case Actions.FIRE:
                str = $"{from} is on fire\n";
                break;
            case Actions.FREEZE:
                str = $"{from} is freese\n";
                break;
            case Actions.HEAL:
                str = $"{from} healed {to} on {hp} hp\n";
                break;
            default:
                break;
        }
        historyDialog.text += str;
        yield return new WaitForSeconds(3.5f);
        historyDialog.text = "Battle log:\n";
    }

    private void ChangeDialog()
    {
        if (state == BattleState.WIN)
            historyDialog.text = "CONGRATULATIONS YOU WIN!";
        if (state == BattleState.LOSE)
            historyDialog.text = "better luck next time...";
    }
}
