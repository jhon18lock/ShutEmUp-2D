using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePingPong : MonoBehaviour
{
    public float speed = 5f;
    public float gradosNegativos = 90f;
    public float grados = 180f;

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(0f, 0f, Mathf.PingPong(Time.time * speed, grados)-gradosNegativos);
    }
}
