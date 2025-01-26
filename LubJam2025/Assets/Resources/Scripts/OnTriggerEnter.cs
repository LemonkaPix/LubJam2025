using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnColliderEnter : MonoBehaviour
{
    public UnityEvent onIn;

    public void OnTriggerEnter(Collider other)
    {
        onIn.Invoke();
    }
}
