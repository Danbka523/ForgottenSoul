using UnityEngine;
public class BattleUI : MonoBehaviour
{
    public GameObject skills;
    public GameObject mainUI;
    public GameObject endGame;
    public GameObject pauseMenu;
    public Unit playerUnit;
    public Unit enemyUnit;

    public void OnSkillsClick()
    {
        skills.SetActive(true);
        mainUI.SetActive(false);
    }

    public void OnBackClick()
    {
        skills.SetActive(false);
        mainUI.SetActive(true);
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
        endGame.SetActive(true);
    }
}