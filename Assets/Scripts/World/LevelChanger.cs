using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] Animator anim;
    public int lvlToLoad;

    private void Start()
    {
        //anim = GetComponent<Animator>();
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

    public void LoadLvl() {
        SceneManager.LoadScene(lvlToLoad);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            FadeToLvl();
    }
}
