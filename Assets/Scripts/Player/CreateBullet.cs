using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBullet : MonoBehaviour
{
    [SerializeField] private GameObject prefab_bullet;
    [SerializeField] private AudioSource audio;

    [SerializeField] AudioClip shoot;
    [SerializeField] AudioClip empty;
    [SerializeField] AudioClip reload_sound;

    [SerializeField] ParticleSystem particle;
    [SerializeField] PlayerMovement player;
    private LineRenderer laser;
    public int ammo;
    private bool isReloading;
    private void Awake()
    {
        ammo = 13;
        laser = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Shoot();
        ShowLaser();
    }
    void Shoot()
    {
        if (!player.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.R)) {
                StartCoroutine("reload");
            }
            if (Input.GetMouseButtonDown(0) && !isReloading)
            {
                if (ammo > 0)
                {
                    GameObject newbullet = Instantiate(prefab_bullet, new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z), transform.rotation);

                    audio.PlayOneShot(shoot);
                    particle.Play();
                    ammo--;
                }
                else {
                    audio.PlayOneShot(empty);
                }
            }
        }
    }

    IEnumerator reload() {
        if (!isReloading)
        {
            isReloading = true;
            audio.PlayOneShot(reload_sound);
            yield return new WaitForSeconds(0.6f);
            ammo = 13;
            isReloading = false;
        }
    }

    void ShowLaser()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit rayHit))
        {
            laser.SetPosition(0, ray.origin);
            laser.SetPosition(1, rayHit.point);
        }

    }

}
