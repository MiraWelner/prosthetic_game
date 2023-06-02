using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  
        This code creates a satisfying explosion on all objects that the rocket is supposed to blow up
*/
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
