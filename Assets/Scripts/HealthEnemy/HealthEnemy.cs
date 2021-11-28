using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{

    private float health = 100f;
    private float damage = 20f;

    public void Damaging()
    {
        if (health <= 20)
        {
            Destroy(this.gameObject);
        }
        else
        {

            health -= damage;

        }

    }
}
