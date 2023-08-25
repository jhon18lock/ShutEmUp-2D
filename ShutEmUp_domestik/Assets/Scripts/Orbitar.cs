using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbitar : MonoBehaviour
{
    public Transform centroOrbita;
    public float radio = 5f;
    public float angulo = 0;
    public float velocidad = 50f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float posX = centroOrbita.position.x + Mathf.Cos(angulo * Mathf.Deg2Rad) * radio;
        float posY = centroOrbita.position.y + Mathf.Sin(angulo * Mathf.Deg2Rad) * radio;
        float posZ = centroOrbita.position.z;

        transform.position = new Vector3(posX, posY, posZ);

        angulo = angulo + velocidad * Time.deltaTime;
    }
}
