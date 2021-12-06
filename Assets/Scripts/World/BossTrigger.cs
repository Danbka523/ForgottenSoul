using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BossTrigger : MonoBehaviour
{
    private LevelChanger levelChanger;
    public Animator anim;

    private void Start()
    {
        anim = GameObject.Find("BlackFade").GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            anim.SetTrigger("fade");
        }
    }
}
