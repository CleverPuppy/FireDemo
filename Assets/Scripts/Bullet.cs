using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public string bulletFlyingSoundName;
    public string bulletHitSoundName;

    AudioSource bulletFlyingSound;
    AudioSource bulletHitSound;

    public GameObject ExplosionEffect;
    ParticleSystem ExplosionEffectSystem;

    public float ExplosionRadius = 2.0f;
    public float baseDamage = 40.0f;
    void Start()
    {
        bulletFlyingSound = GameObject.Find(bulletFlyingSoundName).GetComponent<AudioSource>();
        bulletHitSound = GameObject.Find(bulletHitSoundName).GetComponent<AudioSource>();
        initExplosionEffect();
        onFlying();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Boom!");
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        onHit();
        if(ExplosionEffectSystem)
        {
            Destroy(gameObject, ExplosionEffectSystem.main.duration);
        }
        else
        {
            Destroy(gameObject);
        }

        GameObject[] allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        Vector3 explosionPosition = collision.transform.position;
        for(int i = 0; i < allEnemy.Length; ++i)
        {
            if (Vector3.Distance(explosionPosition, allEnemy[i].gameObject.transform.position) < ExplosionRadius)
            {
                // apply Damage
                float damage = baseDamage;
                applyDamage(allEnemy[i], damage);
            }
        }
    }

    void onFlying()
    {
        bulletFlyingSound.Play();
    }

    void onHit()
    {
        bulletFlyingSound.Stop();
        bulletHitSound.Play();
        playExplosionEffect();
    }

    void initExplosionEffect()
    {
        if (ExplosionEffect)
        {
            ExplosionEffectSystem = ExplosionEffect.GetComponent<ParticleSystem>();
            if(!ExplosionEffectSystem)
                Debug.LogError("ExplosionEffect wrong");
        }else
        {
            Debug.LogWarning("ExplosionEffect not set");
        }
    }

    void playExplosionEffect()
    {
        ExplosionEffectSystem.Play();
    }

    void applyDamage(GameObject enemy , float damage)
    {
        enemy.GetComponent<Enemy>().applyDamage(damage);
        //TODO shou damage UI
    }
}
