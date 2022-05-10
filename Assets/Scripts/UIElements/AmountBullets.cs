using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmountBullets : MonoBehaviour
{
    [SerializeField] private CreateBullet create;
    [SerializeField] private Text text_input;
    void Update()
    {
        text_input.text = create.ammo.ToString() + "/" + "13";
    }
}
