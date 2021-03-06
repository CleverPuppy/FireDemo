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
}
