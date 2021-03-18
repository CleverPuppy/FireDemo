using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health = 100.0f;
    public float death_animate_time = 1.0f;
    public float attack_range = 10.0f;
    public float run_speed = 1.0f;

    public GameObject HitImpactPrefab;
    EnemyAnimationController enemy_ac;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemy_ac = gameObject.GetComponent<EnemyAnimationController>();
        if (!enemy_ac)
        {
            Debug.LogError("Don't hava EnemyAnimationController");
        }
        player = GameObject.Find("Player");


        faceToPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(inAttackRange())
        {
            attack();
        }
        else
        {
            enemy_ac.Run();
            moveToPlayer();
            // RUN To 
        }
    }

    bool shouldDestroy()
    {
        return health <= 0;
    }

    public void applyDamage(float damage_val)
    {
        enemy_ac.Damage();
        health -= damage_val;
        if (shouldDestroy())
        {
            enemy_ac.Death();
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

    void faceToPlayer()
    {
        gameObject.transform.LookAt(player.transform.position);
    }

    void moveToPlayer()
    {
        Vector3 direction;
        direction.x =  player.transform.position.x - gameObject.transform.position.x ;
        direction.z = player.transform.position.z - gameObject.transform.position.z;
        direction.y = 0;
        direction.Normalize();
        gameObject.transform.position += direction * Time.deltaTime * run_speed;
    }

    void attack()
    {
        enemy_ac.Attack();
    }

    bool inAttackRange()
    {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < attack_range)
        {
            return true;
        }

        return false;
    }
}
