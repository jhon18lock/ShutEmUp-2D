
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class OnEnemyTriggerEnterDo : OnTriggerEnterDo
{
    [SerializeField] private UnityEvent noIgnoreActions;


    [SerializeField] string[] tagsToIgnore;

    //protected OVERRIDE, sobreescribir metodo heredado
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // desactivar
        alwaysActions.Invoke();

        foreach (var tag in tagsToIgnore)
        {
            if (collision.CompareTag(tag))
            {
                return;
            }
        }

        noIgnoreActions.Invoke();
    }

    public override void DestroyColisionee()
    {
        Debug.Log("No  deberia hacer nada");
    }
}
