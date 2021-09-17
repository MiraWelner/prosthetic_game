using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitShield : MonoBehaviour
{
    public AudioSource coll;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("shield"))
        {
            Instantiate(coll);
        }
    }
}
