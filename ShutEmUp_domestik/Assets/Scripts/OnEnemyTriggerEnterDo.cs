
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class OnEnemyTriggerEnterDo : OnTriggerEnterDo
{

    //[SerializeField] private UnityEvent alwaysActions;
    [SerializeField] private UnityEvent fxActions;
    [SerializeField] private UnityEvent limitsActions;

    [SerializeField] string tagBullet = "Bullet";

    [SerializeField] string tagLimit = "Limits";

    [SerializeField] HealthController healthController;

    //[SerializeField] EnemyController enemyController;

    //protected OVERRIDE, sobreescribir metodo heredado
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == tagBullet)
        {
            return;
        }

        if (collision.tag == tagLimit)
        {
            limitsActions.Invoke();
        }


        if (healthController != null)
        {
            if(collision.TryGetComponent<HealthController>(out HealthController health))
            {
                health.OnDamage(healthController.health);

                // explosion
                fxActions.Invoke();
            }
        }

        // desactivar
        alwaysActions.Invoke();

    }

    public override void DestroyColisionee()
    {
        Debug.Log("No  deberia hacer nada");
    }
}
