using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    [SerializeField] private Unit player;
    [SerializeField] private Slider slider;

    void Update()
    {
        slider.value = (float)player.currentHp/100;
    }
}
