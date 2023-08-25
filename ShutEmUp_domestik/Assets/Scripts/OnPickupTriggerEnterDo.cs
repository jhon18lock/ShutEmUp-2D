using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnPickupTriggerEnterDo : OnTriggerEnterDo
{

    [SerializeField] private UnityEvent limitsActions;

    [SerializeField] string tagLimit = "Limits";

    //protected OVERRIDE, sobreescribir metodo heredado
    protected override void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == tagLimit)
        {
            // desactivar
            limitsActions.Invoke();
            return;
        }


        // conceder pickup - invocar pickup controller
        alwaysActions.Invoke();

    }

    public override void DestroyColisionee()
    {
        Debug.Log("No  deberia hacer nada");
    }
}
