using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotator : MonoBehaviour
{
    public Vector3 rotationAxis;
    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        //elegir el eje Z
        transform.Rotate(rotationAxis * (rotationSpeed * Time.deltaTime));
    }
}
