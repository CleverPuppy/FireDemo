using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // bullet later
        if(collision.gameObject.layer == 9)
        {
            Debug.Log("Boom!");
            Destroy(collision.gameObject);
        }
    }
}
