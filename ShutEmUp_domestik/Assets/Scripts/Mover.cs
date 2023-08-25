using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public Vector3 direction;

    public float speed;

    //componente que solo mueve objetos

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
