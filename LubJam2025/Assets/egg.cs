using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class egg : MonoBehaviour
{
    [SerializeField] private GameObject particles;
    public void Eggsplosion()
    {
        BallController.instance.rb.isKinematic = true;
        particles.SetActive(true);
    }
}
