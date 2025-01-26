using System.Collections;
using System.Collections.Generic;
using Managers.Sounds;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        bool oldGravity = true; 
        if (collision.gameObject.CompareTag("Player"))
        {
            // print("Collide with player!");
            var rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            collision.gameObject.GetComponent<BallController>().stickParticle.Play();

        }
    }
    
}
