﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressESigns : MonoBehaviour
{
    WeaponPickUp weaponPickUp;
    public GameObject[] signs;
    private void Start()
    {
        weaponPickUp = GetComponent<WeaponPickUp>();
    }
    // Update is called once per frame
    void Update()
    {
        Signs();   
    }
    void Signs()
    {
        signs = GameObject.FindGameObjectsWithTag("Sign");
        int SignCount = GameObject.FindGameObjectsWithTag("Sign").Length;
        if (weaponPickUp.IsInRange())
        {
            if (SignCount <= 0)
            {
              
                
                    
                    Instantiate(weaponPickUp.PressEPrefab, weaponPickUp.weapon.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                
                
            }
                
        }
        else
        {
            foreach (GameObject sign in signs)
                GameObject.Destroy(sign);
        }
    }
}
