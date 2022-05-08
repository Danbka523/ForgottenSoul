using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartText : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
 
    void Update()
    {
        playerMovement.isPaused = true;
        if (Input.anyKeyDown)
        {
            playerMovement.isPaused=false;
            gameObject.SetActive(false);
        }
    }
}
