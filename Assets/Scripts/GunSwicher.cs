using System.Collections;
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
        for(int i = 0; i < Guns.Length; ++i)
        {
            Guns[i].SetActive(false);
        }
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
        // 取消现有枪械的换弹
        Guns[curr_gun_index].GetComponent<Gun>().cancelReload();

        Guns[curr_gun_index].SetActive(false);

        curr_gun_index = index;
        Guns[index].SetActive(true);
    }
}
