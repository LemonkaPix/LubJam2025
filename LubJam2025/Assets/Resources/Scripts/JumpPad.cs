using System.Collections;
using System.Collections.Generic;
using Managers.Sounds;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float Power = 10f;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.forward * Power, ForceMode.Impulse);
            var sm = SoundManager.Instance;
            sm.PlayOneShoot(sm.EnviromentSource, sm.MusicCollection.clips[2]);

        }
    }
}
