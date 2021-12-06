using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBullet : MonoBehaviour
{
    [SerializeField] private GameObject prefab_bullet;
    [SerializeField] private AudioSource audio;
    [SerializeField] ParticleSystem particle;
    [SerializeField] PlayerMovement player;
    private void Update()
    {
        Shoot();
    }
    void Shoot()
    {
        if (!player.isPaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject newbullet = Instantiate(prefab_bullet, new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z), transform.rotation);

                audio.Play();
                particle.Play();
            }
        }
    }

}
