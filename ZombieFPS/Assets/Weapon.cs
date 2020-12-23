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
    [SerializeField] GameObject shell;
    Animator animator;
    bool isFiring;

    Queue<GameObject> shellQueue=new Queue<GameObject>();

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Shoot(rateOfFire));
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            isFiring = true;
            animator.SetBool("isShooting", true);

        }
        else
        {
            isFiring = false;
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
        GameObject hitFX= Instantiate(explosion, hit.point,Quaternion.LookRotation(hit.point));
        Destroy(hitFX, 0.2f);
    }

    private void DamageEnemy(RaycastHit hit)
    {
        var targetEnemy = hit.collider.GetComponent<EnemyAI>();
        targetEnemy.Health = weaponDamage;
    }
}
