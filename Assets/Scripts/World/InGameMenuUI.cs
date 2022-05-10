using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class InGameMenuUI : MonoBehaviour
{
    public GameObject endGame;
    public GameObject pauseMenu;
    public PlayerMovement player;
    public TextMeshProUGUI hpText;
    public Unit playerUnit;
    public GameObject startScreen;


    void Start() {
   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                player.isPaused = false;
            }
            else
            {
                pauseMenu.SetActive(true);
                player.isPaused = true;
            }
        }
      //  hpText.text=$"Current HP:{playerUnit.currentHp}";
        if (playerUnit.isDead)
            EndGame();
    }

    public void OnExitClick() {
        Debug.Log("Clicked");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void OnContinueClick() {
        player.isPaused = false;
        pauseMenu.SetActive(false);
    }

    public void EndGame() {
        endGame.SetActive(true);
        player.isPaused = true;
    }

    public void OnStartClick()
    {
        startScreen.SetActive(false);
        player.isPaused = false;
    }
}
