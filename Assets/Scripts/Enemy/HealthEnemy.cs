using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthEnemy : MonoBehaviour
{

    [SerializeField] private AudioSource audio; //
    [SerializeField] private AudioClip death_clip;
    [SerializeField] AudioClip damage_clip;
    [SerializeField] ParticleSystem blood;

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
        audio.PlayOneShot(damage_clip);
        blood.Play();
        if (health <= 20)
        {
            
            StartCoroutine("destroy");
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

    IEnumerator destroy() {
        audio.PlayOneShot(death_clip);
        yield return new WaitForSeconds(0.8f);
        Destroy(this.gameObject);
    }

}
