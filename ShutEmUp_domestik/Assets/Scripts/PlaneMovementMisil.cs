using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovementMisil : MonoBehaviour
{
    public Transform target;

    public float Speed = 4f;
    public float Acceleration = 2f;

    [SerializeField] Rigidbody2D rb;

    public float RotationControl = 180f;

    float MovY, MovX = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MovY = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        //target - objetivo
        Vector2 direction = transform.position - target.position;
        direction.Normalize();
        float cross = Vector3.Cross(direction, transform.right).z;
        rb.angularVelocity = RotationControl * cross;



        Vector2 Vel = transform.right * (MovX * Acceleration);
        rb.AddForce(Vel);

        float Dir = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.right));

        //if(Acceleration > 0)
        //{
        //    if(Dir > 0)
        //    {
        //        rb.rotation += MovY * RotationControl * (rb.velocity.magnitude / Speed);
        //    }
        //    else
        //    {
        //        rb.rotation -= MovY * RotationControl * (rb.velocity.magnitude / Speed);
        //    }
        //}

        float thrustForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.down)) * 2.0f;

        Vector2 relForce = Vector2.up * thrustForce;

        rb.AddForce(rb.GetRelativeVector(relForce));


        if(rb.velocity.magnitude > Speed)
        {
            rb.velocity = rb.velocity.normalized * Speed;
        }
    }
}
