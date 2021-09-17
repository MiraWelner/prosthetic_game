using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class landRocket : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > 1.969)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 5);
        }
    }
}
