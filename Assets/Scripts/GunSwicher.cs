﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwicher : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] Guns;
    uint curr_gun_index;
    void Start()
    {
        Guns = GameObject.FindGameObjectsWithTag("Gun");
        curr_gun_index = 0;
        changeGun(curr_gun_index);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            changeGun(0);
        }
        if(Input.GetKey(KeyCode.Alpha2))
        {
            changeGun(1);
        }
        if(Input.GetKey(KeyCode.Alpha3))
        {
            changeGun(2);
        }
    }

    void changeGun(uint index)
    {
        for (int i = 0; i < Guns.Length; i++)
        {
            Guns[i].SetActive(i == index ? true : false);
        }
    }
}