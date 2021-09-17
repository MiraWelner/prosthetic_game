using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehavior : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha5))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 20);
        }
        if(Input.GetKeyUp(KeyCode.Alpha5))
        {
            Destroy(this.gameObject);
        }
    }
}
