/*
 * bulletBehavior.java        5/30/23
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  
        This code goes on the 'bullet' object. When the target signal is being sent
        the bullet goes forward, when it is not being sent the bullet dissapears. This
        means that if the child cannot do the target signal for long 'enough' it constitutes
        an incorrect motion. Default target signal is the numerical 5 key
*/

public class bulletBehavior : MonoBehaviour
{    
    public KeyCode signal5 = KeyCode.Alpha5;
    void Update()
    {
        if (Input.GetKey(signal5))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 20);
        }
        if(Input.GetKeyUp(signal5))
        {
            Destroy(this.gameObject);
        }
    }
}
