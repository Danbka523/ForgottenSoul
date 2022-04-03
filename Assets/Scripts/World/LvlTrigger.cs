using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LvlTrigger : MonoBehaviour
{
    [SerializeField]
    LevelChanger levelChanger;
    [SerializeField]
    Animator anim;

    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            levelChanger.LoadLvl();
        }
    }
}
