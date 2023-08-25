using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtSmooth : MonoBehaviour
{
    public Transform target;
    public float speed = 0.1f;


    void Update()
    {
        var targetPosition = FindObjectOfType<Player>().transform.position;
        Vector3 diff = targetPosition - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //rotacion suave ...
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, rot_z), speed * Time.deltaTime);   

        //rotacion automatica brusca
        //transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
