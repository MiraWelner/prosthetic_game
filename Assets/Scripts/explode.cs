using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode : MonoBehaviour
{
    public GameObject explosion;
    public AudioSource explosionSound;
    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Instantiate(explosionSound);
    }
}
