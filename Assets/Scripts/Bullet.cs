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
    void Start()
    {
        bulletFlyingSound = GameObject.Find(bulletFlyingSoundName).GetComponent<AudioSource>();
        bulletHitSound = GameObject.Find(bulletHitSoundName).GetComponent<AudioSource>();
        onFlying();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Boom!");
        onHit();
        Destroy(gameObject);
    }

    void onFlying()
    {
        bulletFlyingSound.Play();
    }

    void onHit()
    {
        bulletFlyingSound.Stop();
        bulletHitSound.Play();
    }
}
