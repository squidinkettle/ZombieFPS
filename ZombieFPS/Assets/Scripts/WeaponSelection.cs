using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelection : MonoBehaviour
{

    [SerializeField] List<GameObject> weapons;
    [SerializeField] int currentWeapon;
  
    float currentValue=0;
    int previousWeapon;
    // Start is called before the first frame update
    void Start()
    {
        for(int x = 1; x < weapons.Count; x++)
        {
            weapons[x].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {


        previousWeapon = currentWeapon;
        ProcessKeyInput();
        ProcessScrollWheel();


    }

    private void ProcessScrollWheel()
    {
        if (Input.GetAxis("MouseScrollWheel") >0)
        {
            if (currentWeapon >= weapons.Count-1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
            UpdateWeapon();
        }


        if (Input.GetAxis("MouseScrollWheel") < 0)
        {
            if (currentWeapon <= 0)
            {
                currentWeapon = weapons.Count-1;
            }
            else
            {
                currentWeapon--;
            }
            UpdateWeapon();
        }
    }

    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
            UpdateWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
            UpdateWeapon();
        }
    }

    void UpdateWeapon()
    {
        weapons[previousWeapon].SetActive(false);
        weapons[currentWeapon].SetActive(true); 
    }


}
