using System;
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
    Animator animator;
    bool isFiring;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Shoot(rateOfFire));
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetButton("Fire1"))
        {
            isFiring = true;
            animator.SetBool("isShooting", true);
        }
        else
        {
            animator.SetBool("isShooting", false);
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
            }
            yield return null;
        }

    }

    private void HitEffect(RaycastHit hit)
    {
        GameObject hitFX= Instantiate(explosion, hit.point,Quaternion.LookRotation(hit.point));
        Destroy(hitFX, 0.2f);
    }

    private void DamageEnemy(RaycastHit hit)
    {
        var targetEnemy = hit.collider.GetComponent<EnemyAI>();
        targetEnemy.Health = weaponDamage;
    }
}
