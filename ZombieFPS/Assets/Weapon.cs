using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeReference] Camera FPCamera;
    [SerializeField] float range=100f;
    [SerializeField] int weaponDamage;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }


    private void Shoot()
    {
        RaycastHit hit;
        Physics.Raycast(FPCamera.transform.position,FPCamera.transform.forward,out hit, range);
        if (hit.collider!=null || hit.collider.GetComponent<EnemyAI>())
        {
            DamageEnemy(hit);
        }
       
    }

    private void DamageEnemy(RaycastHit hit)
    {
        var targetEnemy = hit.collider.GetComponent<EnemyAI>();
        targetEnemy.Health = weaponDamage;
    }
}
