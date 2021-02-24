﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletMode
{
    RayCastingMode,
    CollisionMode,
};

public enum GunFireMode
{
    Scrap,  // 点射
    Burst,  // 连发
};


public class Gun : MonoBehaviour
{
    public float FireRate = 0.2F;
    public int AMOR_PER_PACK = 40;
    public float MAX_RELOAD_TIME = 3.0F;
    public float GunBulletSpeed = 50.0F;
    public float MAX_RANGE = 50.0F;
    public BulletMode bulletMode;

    public GunFireMode fireMode;
    public bool fireModeLocked = true;

    // 自动换弹
    public bool AutoReload = true;

    public GameObject BulletPrefab;
    public GameObject BulletRespawn;
    public GameObject RayStart;

    float reload_time = 0.0f;
    float timelapse_since_last_fire;
    int amor_left;
    bool is_reload = false;
    bool scrap_locked = false;



    void Start()
    {
        timelapse_since_last_fire = FireRate;
        amor_left = AMOR_PER_PACK;
        Debug.Log(BulletRespawn.transform);
    }

    // Update is called once per frame
    void Update()
    {
        update_time();
        if (is_reload)
        {
            onReload();
        } else {
            if (fireMode == GunFireMode.Scrap)
            {
                // 单发
                if (Input.GetMouseButtonDown(0) && !scrap_locked)
                {
                    fire();
                    scrap_locked = true;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    scrap_locked = false;
                }
            }
            if (fireMode == GunFireMode.Burst)
            {
                // 连发
                if (Input.GetMouseButton(0))
                {
                    // fire
                    fire();
                }
            }

            if(Input.GetMouseButton(1))
            {
                startReload();
            }
        }

        // 按V键更改设计模式
        if (!fireModeLocked && 
            Input.GetKey(KeyCode.V))
        {
            swapFireMode();
        }
    }

    void startReload()
    {
        Debug.Log("Reloading!");
        reload_time = 0.0f;
        is_reload = true;
    }

    void onReload()
    {
        if(reload_time >= MAX_RELOAD_TIME){
            is_reload = false;
            amor_left = AMOR_PER_PACK;
            Debug.Log("reloaded!");
        }
    }

    void cancelReload()
    {
        reload_time = 0.0F;
        is_reload = false;
    }

    void update_time()
    {
        // 更新换弹CD
        if(is_reload)
        {
            reload_time += Time.deltaTime;
        }

        // 更新射击CD
        timelapse_since_last_fire += Time.deltaTime;
    }

    void fire()
    {
        if(timelapse_since_last_fire >= FireRate && amor_left > 0)
        {
            Debug.Log("Fire!");
            switch (bulletMode)
            {
                case BulletMode.CollisionMode:
                    var bullet = Instantiate(BulletPrefab);
                    var rigidbody = bullet.GetComponent<Rigidbody>();
                    rigidbody.position = BulletRespawn.transform.position;
                    rigidbody.rotation = BulletRespawn.transform.rotation;
                    rigidbody.velocity = transform.TransformDirection(Vector3.up * GunBulletSpeed);
                    break;
                case BulletMode.RayCastingMode:                    
                    Ray ray = new Ray(BulletRespawn.transform.position, BulletRespawn.transform.position
                                                                            - RayStart.transform.position);
                    RaycastHit hitInfo;
                    if(Physics.Raycast(ray, out hitInfo, maxDistance : MAX_RANGE))
                    {
                        Debug.Log("Ray Hit is " + hitInfo.collider.gameObject.name);
                        Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1F);
                    }
                    break;
            }


            timelapse_since_last_fire = 0.0F;
            amor_left -= 1;
        }

        if(AutoReload && amor_left == 0)
        {
            startReload();
        }
    }

    void swapFireMode()
    {
        if (fireMode == GunFireMode.Scrap)
        {
            fireMode = GunFireMode.Burst;
        }
        else if (fireMode == GunFireMode.Burst)
        {
            fireMode = GunFireMode.Scrap;
        }
    }
}