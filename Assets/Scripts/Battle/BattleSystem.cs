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
    public TextMeshProUGUI currentHP;
    public TextMeshProUGUI currentSP;

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

    System.Random random = new System.Random();

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
        playerUnit.AddSp(7);
        enemyUnit.AddSp(7);
        currentHP.text = $"Current HP:{playerUnit.currentHp}";
        currentSP.text = $"Current SP:{playerUnit.currentSp}";
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

    public void heal() {
        Actions action = Actions.HEAL;
        if (state == BattleState.PLAYERTURN)
        {
            playerUnit.HealUnit(30);
            ChangeDialog(action, playerUnit.unitName, enemyUnit.unitName, 30);
            state = BattleState.ENEMYTURN;
            UnitTurn();
        }

    }

    private void enemey_heal() {
        Actions action = Actions.HEAL;
        enemyUnit.HealUnit(30);
        ChangeDialog(action, enemyUnit.unitName, playerUnit.unitName, 30);
        state = BattleState.PLAYERTURN;
        UnitTurn();
    }


    //обычная атака
    public void PhysAttack()
    {
        Actions action = Actions.DAMAGE;
        if (state == BattleState.PLAYERTURN)
        {
            enemyUnit.physEffect.Play();
            enemyUnit.DealPhysDamage(playerUnit.damage,out realDamage);
            ChangeDialog(action, playerUnit.unitName, enemyUnit.unitName, realDamage);
            state = BattleState.ENEMYTURN;
            UnitTurn();
        }
    }

   private void EnemyPhysAttack() {
        playerUnit.physEffect.Play();
        Actions action = Actions.DAMAGE;
        playerUnit.DealPhysDamage(enemyUnit.damage, out realDamage);
        ChangeDialog(action, enemyUnit.unitName, playerUnit.unitName, realDamage);
        state = BattleState.PLAYERTURN;
        UnitTurn();
    }


    //fire
    //========================//
    public void FireAttack()
    {
        Actions action = Actions.DAMAGE;
        if (state == BattleState.PLAYERTURN)
        {
            enemyUnit.fireAttEffect.Play();
            enemyUnit.DealFireDamage(playerUnit.damage, roundCounter, out realDamage);
            ChangeDialog(action, playerUnit.unitName, enemyUnit.unitName, realDamage);
            state = BattleState.ENEMYTURN;
            UnitTurn();
        }
    }

    private void EnemyFireAttack() {
        playerUnit.fireAttEffect.Play();
        Actions action = Actions.DAMAGE;
        playerUnit.DealFireDamage(enemyUnit.damage, roundCounter, out realDamage);
        ChangeDialog(action, enemyUnit.unitName, playerUnit.unitName, realDamage);
        state = BattleState.PLAYERTURN;
        UnitTurn();
    }


    private void CheckFire()
    {
        //проверка игрока
        if (playerUnit.isFired)
        {
            playerUnit.fireEffect.Play();
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
            enemyUnit.fireEffect.Play();
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


    
    //ice
    //========================//
    public void IceAttack()
    {
        Actions action = Actions.DAMAGE;
        if (state == BattleState.PLAYERTURN)
        {
            enemyUnit.frozenAttEffect.Play();
            enemyUnit.DealIceDamage(playerUnit.damage, roundCounter,out realDamage);
            ChangeDialog(action, playerUnit.unitName, enemyUnit.unitName, realDamage);
            state = BattleState.ENEMYTURN;
            UnitTurn();
        }
    }

    private void EnemyIceAttack() {
        playerUnit.frozenAttEffect.Play();
        Actions action = Actions.DAMAGE;
        playerUnit.DealIceDamage(enemyUnit.damage, roundCounter, out realDamage);
        ChangeDialog(action, enemyUnit.unitName, playerUnit.unitName, realDamage);
        state = BattleState.PLAYERTURN;
        UnitTurn();
    }


    private void CheckFreeze()
    {
        //проверка игрока
        if (playerUnit.isFreezed && state == BattleState.PLAYERTURN)
        {
            playerUnit.frozenEffect.Play();
            state = BattleState.ENEMYTURN;
            if (roundCounter - playerUnit.negativeEffectRound == 2)
            {
                playerUnit.isFreezed = false;
                playerUnit.isNegatived = false;
            }
        }
        //проверка противнкиа
        if (enemyUnit.isFreezed && state == BattleState.ENEMYTURN)
        {
            enemyUnit.frozenEffect.Play();
            state = BattleState.PLAYERTURN;
            if (roundCounter - enemyUnit.negativeEffectRound == 2)
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
        if (enemyUnit.currentHp < 40)
            enemey_heal();
        int type = random.Next(2);
        if (enemyUnit.currentSp > 30)
        switch (type)
        {
            case 0:
                EnemyFireAttack();
                    break;
            case 1:
                EnemyIceAttack();
                    break;
            default:
                break;
        }
        else 
            EnemyPhysAttack(); 
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
                str = $"{from} healed on {hp} hp\n";
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
     
    }
}
