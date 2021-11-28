using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBullet : MonoBehaviour
{
    [SerializeField] private GameObject prefab_bullet;
    private AudioSource audio;
    private void Start()
    {
        audio =  gameObject.GetComponent<AudioSource>();
    }
    private void Update()
    {
        Shoot();
    }
    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newbullet = Instantiate(prefab_bullet, transform.position, transform.rotation);

            audio.Play();
        }
    }

}
