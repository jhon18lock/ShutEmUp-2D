using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MisilKinematic : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public float speed = 5f;
    public float RotationControl = 200f;

    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        rb.isKinematic = true;
    }

    
    private void FixedUpdate()
    {
        //target - objetivo
        Vector2 direction = transform.position - target.position;
        direction.Normalize();
        float cross = Vector3.Cross(direction, transform.right).z;
        rb.angularVelocity = RotationControl * cross;

        rb.velocity = transform.right * speed;
    }
}
