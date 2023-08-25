using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContadorHijos : MonoBehaviour
{
    public int hijos;
    public float angulos;

    // Start is called before the first frame update
    void Start()
    {
        hijos = transform.childCount;
        Debug.Log("Cantidad de orbitas: " + hijos);
        angulos = 360 / hijos;
        Debug.Log("Angulos: " + angulos);
        CalcularAngulos();
    }

    public void CalcularAngulos()
    {
        for (int i = 0; i < hijos; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Orbitar>().angulo = angulos * (i + 1);
            //transform.GetChild(i).gameObject.GetComponent<Orbitar>().velocidad = 25 * (i+1);
        }
    }
}
