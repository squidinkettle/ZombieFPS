﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeReference] Camera FPCamera;
    [SerializeField] float range=100f;
    [SerializeField] int weaponDamage;
    [SerializeField] ParticleSystem muzzle;
    [SerializeField] GameObject explosion;
    [SerializeField] float rateOfFire;
    [SerializeField] GameObject shell;
    [SerializeField] int maxAmmo;
    int ammo;
    [SerializeField] int maxClips;
    int clips;
    bool isReloading;
    Animator animator;
    bool isFiring;

    [SerializeField] float reloadingTime;

    Queue<GameObject> shellQueue=new Queue<GameObject>();

    private void Start()
    {
        ammo = maxAmmo;
        clips = maxClips;
        animator = GetComponent<Animator>();
        StartCoroutine(Shoot(rateOfFire));
        StartCoroutine(Reloading(reloadingTime));
    }

    public int Ammo
    {
        get { return ammo; }
        set { ammo = value; }

    }


    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        FireWeapon();
        ReloadWeapon();
    }

    private void ReloadWeapon()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (ammo < maxAmmo)
            {
                isReloading = true;
            }
        }
    }



    private void FireWeapon()
    {
        if (Input.GetButton("Fire1"))
        {
            if (ammo > 0 && !isReloading)
            {
                isFiring = true;
                animator.SetBool("isShooting", true);
            }

        }
        else
        {
            isFiring = false;
            animator.SetBool("isShooting", false);
        }
    }

    IEnumerator Reloading(float reloadTime)
    {

        while (true)
        {
            while (isReloading)
            {

                yield return new WaitForSeconds(reloadTime);
                isReloading = false;
                ammo = maxAmmo;
                clips--;
            }




            yield return null;
        }
    }

    IEnumerator Shoot(float RoF)
    {
        while (true)
        {
            while (isFiring)
            {

                RaycastHit hit;
                Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range);
                ShellQueueCicle();
                muzzle.Play();
                if (hit.collider != null && hit.collider.GetComponent<EnemyAI>())
                {
                    DamageEnemy(hit);
                }
                else
                {
                    HitEffect(hit);
                }
                yield return new WaitForSeconds(RoF);
                ammo--;
                animator.SetBool("isShooting", false);
            }
            yield return null;
        }

    }

    private void ShootOutShell()
    {
        float yOffset = 0.01f;
        float xOffset = 0.04f;
        Vector3 shellPosition = new Vector3(transform.position.x+xOffset, transform.position.y + yOffset, transform.position.z);
        GameObject newShell = Instantiate(shell, shellPosition, Quaternion.identity);

        shellQueue.Enqueue(newShell);
        SetShellStartingPosition(newShell);



    }

    void ShellQueueCicle()
    {
        int maxNumShells = 10;
        if (shellQueue.Count >= maxNumShells)
        {
            var oldShell = shellQueue.Dequeue();
            SetShellStartingPosition(oldShell);


            shellQueue.Enqueue(oldShell);


                
        }
        else
        {
            ShootOutShell();
        }


    }

    private void SetShellStartingPosition(GameObject showShell)
    {
        float popSpeed = 10f;
        float yOffset = 0.01f;
        float xOffset = 0.04f;
        Vector3 shellPosition = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z);

        showShell.transform.position=shellPosition;
        showShell.transform.rotation = Quaternion.identity;
        Rigidbody shellPhysics = showShell.GetComponent<Rigidbody>();

        shellPhysics.AddForce(transform.up * popSpeed);
        shellPhysics.AddForce(transform.right * popSpeed);

    }

    private void HitEffect(RaycastHit hit)
    {
        if (hit.distance == 0) { return; }

        GameObject hitFX= Instantiate(explosion, hit.point, Quaternion.LookRotation(hit.point));
        Destroy(hitFX, 0.2f);
    }

    private void DamageEnemy(RaycastHit hit)
    {
        var targetEnemy = hit.collider.GetComponent<EnemyAI>();
        targetEnemy.Health = weaponDamage;
    }
}
