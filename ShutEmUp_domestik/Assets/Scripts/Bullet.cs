using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class Bullet : OnTriggerEnterDo
{
    [SerializeField] Mover mover;

    [SerializeField] BulletConfig config;


    //[SerializeField] private UnityEvent alwaysActions;
    //[SerializeField] private UnityEvent alwaysActions;

    [SerializeField] private UnityEvent unignoredActions;

    [SerializeField] List<string> tagsToIgnore;


    //public Mover Mover { get { return mover; } set { mover = value; } }

    private void Start()
    {
        if(config != null)
        {
            mover.speed = config.speed;

        }
    }

    //protected OVERRIDE, sobreescribir metodo heredado
    protected override void OnTriggerEnter2D(Collider2D collision)
    {

        //ejecutar siempre
        alwaysActions.Invoke();
        //si la colision esta dentro de los tags a ignorar
        //salir, NO hacer nada
        foreach (var item in tagsToIgnore)
        {
            if (collision.tag == item)
            {
                return;
            }
        }
        //de lo contrario invocar evento
        unignoredActions.Invoke();

        if (collision.TryGetComponent<HealthController>(out HealthController hc))
        {
            hc.OnDamage(config.damage);
        }

    }


}
