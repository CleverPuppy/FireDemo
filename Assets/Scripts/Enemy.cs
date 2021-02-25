using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health = 100.0f;
    public float death_animate_time = 1.0f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldDestroy())
        {
            Object.Destroy(gameObject, death_animate_time);
        }
    }

    bool shouldDestroy()
    {
        return health <= 0;
    }
}
