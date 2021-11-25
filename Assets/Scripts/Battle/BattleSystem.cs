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
    Unit EnemyUnit;

    //BattleState
    public BattleState state;

    void Start()
    {
        state = BattleState.PLAYERTURN;
        playerUnit = playerPrefab.GetComponent<Unit>();
        EnemyUnit = enemyPrefab.GetComponent<Unit>();
    }


    public void PlayerAttack() {
        if (state != BattleState.PLAYERTURN)
            return;
        EnemyUnit.DealPhysDamage(playerUnit.damage);
        StartCoroutine(ChangeDialog("damage","player","enemy",playerUnit.damage));
        if (EnemyUnit.isDead)
        {
            state = BattleState.WIN;
            ChangeDialog(state);
        }
       // else
           // state = BattleState.ENEMYTURN;

    }


   public void EnemyTurn() { }


    IEnumerator ChangeDialog(string action, string from, string to, int hp) {
        string str = $"{from} damaged {to} on {hp} hp\n";
        if (action == "damage")
            historyDialog.text += str;
        yield return new WaitForSeconds(5f);
        historyDialog.text = "Battle log:\n";
    }

    void ChangeDialog(BattleState state )
    {
        if (state == BattleState.WIN)
            historyDialog.text = "CONGRATULATIONS YOU WIN!";
        if (state == BattleState.LOSE)
            historyDialog.text = "better luck next time...";
    }
}
