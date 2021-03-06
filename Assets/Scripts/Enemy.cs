using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health = 100.0f;
    public float death_animate_time = 1.0f;

    public GameObject HitImpactPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool shouldDestroy()
    {
        return health <= 0;
    }

    public void applyDamage(float damage_val)
    {
        health -= damage_val;
        if (shouldDestroy())
        {
            Object.Destroy(gameObject, death_animate_time);
        }
    }

    public void applyDamageAndShowImpact(float damage_val, Vector3 position, Quaternion rotation)
    {
        applyDamage(damage_val);
        showHitImpact(position, rotation);
    }

    void showHitImpact(Vector3 position, Quaternion rotation)
    {
        GameObject hitImpact =  GameObject.Instantiate(HitImpactPrefab, position, rotation,gameObject.transform);
        ParticleSystem hitEffectSystem = hitImpact.GetComponent<ParticleSystem>();
        hitEffectSystem.Play();
    }
}
