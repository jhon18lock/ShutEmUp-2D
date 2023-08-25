using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //[HideInInspector]
    public EnemyConfig config;

    [SerializeField] private SpriteRenderer spriteRend;
    [SerializeField] private MultipleInstantiator multipleInstantiator;
    //[SerializeField] HealthController healthController;
    private Mover mover;
    private Shooter[] shooters;

    Coroutine shooterCoroutine;


    private void Start()
    {
        mover = GetComponent<Mover>();

        if(mover != null && config != null)
        {
            mover.speed = config.moverSpeed;
        }

        if(config.sprite != null && spriteRend != null )
        {
            spriteRend.sprite = config.sprite;
        }

        // obtener shooters
        shooters = GetComponentsInChildren<Shooter>();

        if (shooters != null && shooters.Length > 0)
        {
            foreach (var shooter in shooters)
            {

                shooterCoroutine = StartCoroutine(ShootForever(shooter));
            }
        }

    }

    private void OnEnable()
    {
        if (shooters != null && shooters.Length > 0)
        {
            foreach (var shooter in shooters)
            {

                shooterCoroutine = StartCoroutine(ShootForever(shooter));
            }
        }
    }

    //llamar funcion desde eventos del inspector de OnEnemyTrigger
    public void OnDie()
    {
        //Debug.Log("I'm Dead! Funcion OnDie Enemy Controller");

        //si hay config y si se debe lanzar pickup
        if(config != null && config.shouldThrowPickup())
        {
            //Debug.Log("EnemyControler: OnDie: hay Enemy config y se lanza pickup!!");
            //si hay instanciador multiple
            if(multipleInstantiator != null)
            {
                if(multipleInstantiator.InstantiatorsCount > 1)
                {
                    Debug.Log("enemycontroller: tengo mas de un instanciador de pickup");
                    for (int i = 0; i < multipleInstantiator.InstantiatorsCount; i++)
                    {
                        if (DiceDado.IsChanceSuccess(config.pickupChance))
                        {
                            multipleInstantiator.InstantiateByIndex(i);

                        }
                    }
                }
                else
                {
                    multipleInstantiator.InstantiateInSequence();
                }

            }
        }
        //llamar al metodo OnDie de GameController
        GameController.Instance.OnDie(gameObject, config.score);
        
        if(shooterCoroutine != null)
        {
            StopCoroutine(shooterCoroutine);
        }

    }

    private IEnumerator ShootForever(Shooter shooter)
    {
        yield return new WaitForSeconds(shooter.ShootingConfig.shootInitialWaitTime);
        while (true)
        {
            // si se dispara por cantidad
            if (shooter.ShootingConfig.waitPerShootings)
            {
                // cantidad de disparos - for para cada shooter independiente
                // usar foreach para sincronizar disparos
                for (int i = 0; i < shooter.ShootingConfig.shootsAmount; i++)
                {
                    shooter.DoShoot();
                    //yield return new WaitForSeconds(config.shootCadence);
                    yield return new WaitForSeconds(shooter.ShootingConfig.shootCadence);

                }
                // esperar tiempo para las siguientes rafagas
                yield return new WaitForSeconds(shooter.ShootingConfig.waitTimePerShootings + Random.Range(0.1f, 0.5f));
            }
            else
            {
                shooter.DoShoot();
                //yield return new WaitForSeconds(config.shootCadence);
                yield return new WaitForSeconds(shooter.ShootingConfig.shootCadence);
            }
        }
    }

    // Funciones para los eventos de colision

    public void MuertePorLimites()
    {
        GameController.Instance.MuertePorLimites();
    }

    public void DesactivarEnemigo()
    {
        ObjectPoolManager.ReturnEnemyToPool(this);
    }
}
