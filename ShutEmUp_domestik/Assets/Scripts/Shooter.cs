using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] Transform shootOrigin;
    [SerializeField] GameObject shootPrefab;
    [SerializeField] private ShootingConfig config;
    public ShootingConfig ShootingConfig { get { return config; } }

    public bool IsEnabled = true;

    private void OnEnable()
    {
        if(config == null) { return; }

        if (config.autoShooting)
        {
            StartCoroutine(AutoShoot());
        }
    }

    private IEnumerator AutoShoot()
    {
        while (true)
        {
            DoShoot();

            yield return new WaitForSeconds(config.shootCadence);
        }
    }

    public void DoShoot()
    {
        if (IsEnabled)
        {

            GameObject bullet = ObjectPoolManager.SpawnObject(shootPrefab, transform.position, transform.rotation);

            if( bullet.TryGetComponent<Bullet>(out Bullet b))
            {
                b.damage = config.damage;
                b.Mover.speed = config.speed;
            }


            //obtener un ObjectPooling bullet
        }
    }

    public void EnableShooter(bool shouldEnable)
    {
        IsEnabled = shouldEnable;
    }

    //private void OnEnable()
    //{
    //    StartCoroutine(AutoShoot());
    //}

    private void OnDisable()
    {
        StopCoroutine(AutoShoot());
    }
}
