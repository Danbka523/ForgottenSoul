using System.Collections;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOSE };
public enum Actions { DAMAGE, BLOCK, MISS, HEAL, FIRE, FREEZE }

public class BattleSystem : MonoBehaviour
{   //UI
    public TextMeshProUGUI historyDialog;
    public TextMeshProUGUI turnDialog;
    private List<string> historyMSGs;
    private int maxMSGs;

    //Objects
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    //Units
    Unit playerUnit;
    Unit enemyUnit;

    //BattleState
    public BattleState state;
    public int roundCounter;

    private int realDamage;

    void Start()
    {
        historyMSGs = new List<string>();
        maxMSGs = 5;
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
        {
            turnDialog.text = $"Current turn:{playerUnit.unitName}";
            roundCounter++;
        }
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
        {
            turnDialog.text = $"Current turn:{enemyUnit.unitName}";
            StartCoroutine(EnemyTurn());
        }
    }

    //обычная атака
    public void PhysAttack()
    {
        Actions action = Actions.DAMAGE;
        if (state == BattleState.PLAYERTURN)
        {
            enemyUnit.DealPhysDamage(playerUnit.damage,out realDamage);
            ChangeDialog(action, playerUnit.unitName, enemyUnit.unitName, realDamage);
            state = BattleState.ENEMYTURN;
            UnitTurn();
        }
        else if (state == BattleState.ENEMYTURN)
        {
            playerUnit.DealPhysDamage(enemyUnit.damage,out realDamage);
            ChangeDialog(action, enemyUnit.unitName, playerUnit.unitName, realDamage);
            state = BattleState.PLAYERTURN;
            UnitTurn();
        }
        else
            return;
    }
    //огненная атака (wip)
    //fire
    //========================//
    public void FireAttack()
    {
        Actions action = Actions.DAMAGE;
        if (state == BattleState.PLAYERTURN)
        {
            enemyUnit.DealFireDamage(playerUnit.damage, roundCounter, out realDamage);
            ChangeDialog(action, playerUnit.unitName, enemyUnit.unitName, realDamage);
            state = BattleState.ENEMYTURN;
            UnitTurn();
        }
        else if (state == BattleState.ENEMYTURN)
        {
            playerUnit.DealFireDamage(enemyUnit.damage, roundCounter,out realDamage);
            ChangeDialog(action, enemyUnit.unitName, playerUnit.unitName,realDamage);
            state = BattleState.PLAYERTURN;
            UnitTurn();
        }
        else
            return;
    }

    private void CheckFire()
    {
        //проверка игрока
        if (playerUnit.isFired)
        {
            playerUnit.DealDamage(8);
            ChangeDialog(Actions.FIRE, "", playerUnit.unitName);
            if (roundCounter - playerUnit.negativeEffectRound == 3)
            {
                playerUnit.isFired = false;
                playerUnit.isNegatived = false;
            }
        }
        //проверка противнкиа
        if (enemyUnit.isFired)
        {
            ChangeDialog(Actions.FIRE, "", enemyUnit.unitName);
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


    //ледяная атака(wip)
    //ice
    //========================//
    public void IceAttack()
    {
        Actions action = Actions.DAMAGE;
        if (state == BattleState.PLAYERTURN)
        {
            enemyUnit.DealIceDamage(playerUnit.damage, roundCounter,out realDamage);
            ChangeDialog(action, playerUnit.unitName, enemyUnit.unitName, realDamage);
            state = BattleState.ENEMYTURN;
            UnitTurn();
        }
        else if (state == BattleState.ENEMYTURN)
        {
            playerUnit.DealIceDamage(enemyUnit.damage, roundCounter,out realDamage);
            ChangeDialog(action, enemyUnit.unitName, playerUnit.unitName, realDamage);
            state = BattleState.PLAYERTURN;
            UnitTurn();
        }
        else
            return;
    }

    private void CheckFreeze()
    {
        //проверка игрока
        if (playerUnit.isFreezed && state == BattleState.PLAYERTURN)
        {
            state = BattleState.ENEMYTURN;
            if (roundCounter - playerUnit.negativeEffectRound == 3)
            {
                playerUnit.isFreezed = false;
                playerUnit.isNegatived = false;
            }
        }
        //проверка противнкиа
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



    //ход противника (wip)
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
    private void ChangeDialog(Actions action, string from = "", string to = "", int hp = 0)
    {
        string str = "";
        switch (action)
        {
            case Actions.DAMAGE:
                str = $"{from} damaged {to} on {hp} hp\n";
                break;
            case Actions.FIRE:
                str = $"{to} is on fire\n";
                break;
            case Actions.FREEZE:
                str = $"{to} is freese\n";
                break;
            case Actions.HEAL:
                str = $"{from} healed {to} on {hp} hp\n";
                break;
            default:
                break;
        }
        historyMSGs.Add(str);
        historyDialog.text += str;
        if (historyMSGs.Count >= maxMSGs) {
            historyDialog.text=historyDialog.text.Remove(historyDialog.text.IndexOf(historyMSGs[0]), historyMSGs[0].Length);
            historyMSGs.Remove(historyMSGs[0]);
            historyDialog.ForceMeshUpdate();
        }
    }

    private void ChangeDialog()
    {
        if (state == BattleState.WIN)
            historyDialog.text = "CONGRATULATIONS YOU WIN!";
        if (state == BattleState.LOSE)
            historyDialog.text = "better luck next time...";
    }
}
