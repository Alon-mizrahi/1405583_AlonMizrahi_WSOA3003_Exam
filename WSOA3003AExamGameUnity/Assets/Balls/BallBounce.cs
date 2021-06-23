using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour
{

    Rigidbody rb;
    Vector3 LastVelocity;
    public float BounceLoss=0.7f;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        LastVelocity = rb.velocity;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "ThroughWall")
        {
            float speed = LastVelocity.magnitude;
            Vector3 direction = Vector3.Reflect(LastVelocity.normalized, collision.GetContact(0).normal);

            rb.velocity = direction * Mathf.Max(speed, 0f) * BounceLoss;
        }

        /*
        if (collision.gameObject.tag == "PowerBall" || collision.gameObject.tag == "TargetBall")
        {
            float speed = LastVelocity.magnitude;
            Vector3 direction = Vector3.Reflect(LastVelocity.normalized, collision.GetContact(0).normal);

            rb.velocity = direction * Mathf.Max(speed, 0f) * BounceLoss;
        }
        */
    }
}
