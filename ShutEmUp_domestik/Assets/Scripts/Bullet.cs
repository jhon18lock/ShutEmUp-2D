using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    [SerializeField] Mover mover;

    public float damage;

    public float speed = 5f;

    //[SerializeField] private UnityEvent alwaysActions;
    [SerializeField] private UnityEvent alwaysActions;

    [SerializeField] private UnityEvent unignoredActions;

    [SerializeField] List<string> tagsToIgnore;


    public Mover Mover { get { return mover; } set { mover = value; } }


    //protected OVERRIDE, sobreescribir metodo heredado
    void OnTriggerEnter2D(Collider2D collision)
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
            hc.OnDamage(damage);
        }

    }


}
