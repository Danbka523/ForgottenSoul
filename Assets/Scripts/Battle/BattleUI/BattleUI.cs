using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class BattleUI : MonoBehaviour
{
    public GameObject items;
    public GameObject skills;
    public GameObject mainUI;
    public GameObject endGame;
    public GameObject pauseMenu;
    public Unit playerUnit;
    public Unit enemyUnit;
    public GameObject errMsg;

    public void OnSkillsClick()
    {
        skills.SetActive(true);
        mainUI.SetActive(false);
    }

    public void OnBackClick()
    {
        //playerUnit.inventory.inventoryUI.CreateUI();
        skills.SetActive(false);
        items.SetActive(false);
        mainUI.SetActive(true);
    }

    public void OnItemsClick() {
        //items.SetActive(true);
        StartCoroutine("ErrMsg");
        //mainUI.SetActive(false);
    }

    IEnumerator ErrMsg() { 
        errMsg.SetActive(true);
        yield return new WaitForSeconds(2f);
        errMsg.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
            }
            else
            {
                pauseMenu.SetActive(true);
            }
        }
        if (playerUnit.isDead ||enemyUnit.isDead )
            EndGame();
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void OnContinueClick()
    {
        pauseMenu.SetActive(false);
    }

    public void EndGame()
    {
        SceneManager.LoadScene(7);
    }

}