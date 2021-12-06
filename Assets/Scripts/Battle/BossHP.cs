using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHP : MonoBehaviour
{
    public Slider bossHP;
    public Unit bossUnit;

    private void Start()
    {
        bossHP.maxValue = bossUnit.maxHp;
    }

    private void Update()
    {
        bossHP.value = bossUnit.currentHp;
    }
}
