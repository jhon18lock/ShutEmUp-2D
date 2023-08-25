using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DiceDado
{
    // Dice (dado en ingles)
    //su funcion es devolver un valor dado una probabilidad

    //[Range(0,1f)]
    //public float chance;

    public static bool IsChanceSuccess(float chance)
    {
        //si el valor (la probabilidad pasada como parametro) es cero
        //retornar falso (no arroja pickup)
        if(chance == 0f)
        {
            return false;
        }
        var randomValue = Random.Range(0, 1f);
        //Debug.Log("dado: " + randomValue);
        return (chance >= randomValue) ;
    }
}
