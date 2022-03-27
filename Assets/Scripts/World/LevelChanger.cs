using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private Animator anim;
    public int lvlToLoad;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FadeToLvl() {
        anim.SetTrigger("fade");
    }

    public void OnFadeComplete() {
        SceneManager.LoadScene(lvlToLoad);
    }

    public void OnFadeCompleteExit() {
        Application.Quit();
    }

    public void LoadStartLvl() {
        SceneManager.LoadScene(2);
    }
}
