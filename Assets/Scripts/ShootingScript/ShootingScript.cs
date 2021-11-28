using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [SerializeField] private GameObject prefab_decal;
    [SerializeField] private float bullet_velocity;
    private float _time_for_destroy_decal = 2.0f;
    private float _time_for_destroy_bullet = 3.0f;
    private Vector3 lastPos;
    private HealthEnemy _health;

    private void Awake()
    {
        lastPos = transform.position;
    }
    private void Update()
    {
        ShootBullet();
    }

    void ShootBullet()
    {

        transform.Translate(Vector3.forward * bullet_velocity * Time.deltaTime);
        RaycastHit hit;
        if (Physics.Linecast(lastPos, transform.position, out hit))
        {
            _health = hit.transform.GetComponent<HealthEnemy>();
            if (_health != null)
            {
                _health.Damaging();
            }
            else
            {
                Quaternion rotation_decal = Quaternion.FromToRotation(Vector3.up, hit.normal);
                GameObject d = Instantiate(prefab_decal, hit.point, rotation_decal);

                Destroy(d, _time_for_destroy_decal);
            }
            Destroy(gameObject);

        }
        else
        {
            Destroy(gameObject,_time_for_destroy_bullet);
        }

       
        lastPos = transform.position;
    }
}
