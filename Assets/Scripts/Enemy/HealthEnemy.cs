using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthEnemy : MonoBehaviour
{
    public Slider hpSlider;
    public Unit playerUnit;

    private int health = 100;
    private int enemyDamage = 20;

    private void Start()
    {
        hpSlider.maxValue = health;
    }


    public void Damaging(int damage)
    {
        if (health <= 20)
        {
            Destroy(this.gameObject);
        }
        else
        {
            health -= damage;
            hpSlider.value = health;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") 
            playerUnit.DealDamage(enemyDamage);
   
    }
}
