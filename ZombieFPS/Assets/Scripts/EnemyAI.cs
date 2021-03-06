﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    Player player;
    Transform target;
    Vector3 moveObjective=new Vector3();
    [SerializeField] float turnSpeed=10;
    [SerializeField] float range;
    [SerializeField]int damage;
    [Range(0.1f,3f)][SerializeField] float damageTimer;
    [SerializeField] int maxHealth;
    Animator animator;
    bool isDead;



    NavMeshAgent navMeshAgent;
    float distance = Mathf.Infinity;

    bool isProvoked;
    bool canAttack;
    private int _health;

    public int Health
    {
        get { return _health; }
        set { _health -= value;
            OnDamageTaken();
            StartDeath();

              }
        
    }


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        target = player.transform;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(AttackPerSecond(damageTimer));
        _health = maxHealth;

    }


    void StartDeath()
    {

        if (_health <= 0)
        {
            animator.Play("Base Layer.Dead");
            Destroy(GetComponent<CapsuleCollider>());
            navMeshAgent.isStopped = true;
            isDead = true;
            animator.SetBool("isDead", true);
            animator.SetBool("idle", false);
            animator.SetBool("moving", false);
            animator.SetBool("attack", false);

            Destroy(gameObject,5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (animator == null) { return; }
            distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance < range)
            {

                moveObjective = target.transform.position;
                isProvoked = true;
                animator.SetBool("idle", false);
            }



            if (isProvoked)
            {
                print("engaging Target");
                EngageTarget();

            }
            else
            {
                animator.SetBool("idle", true);
            }



        }

    }

    private void EngageTarget()
    {
        if (isDead) { return; }
        Chase();

    }

    private void Chase()
    {
        if (isDead) { return; }
        animator.SetBool("moving", true);
    
        navMeshAgent.SetDestination(moveObjective);
        float distanceToDestination = Vector3.Distance(transform.position, moveObjective);

        if (distance < navMeshAgent.stoppingDistance)
        {
            FaceTarget();
            canAttack = true;
        }
        else if (distanceToDestination <= navMeshAgent.stoppingDistance)
        {
         
            animator.SetBool("moving", false);
            isProvoked = false;
        }
        else
        {
            canAttack = false;
            animator.SetBool("attack", false);
        }
    }

    private void FaceTarget()
    {
        
        Vector3 faceDir = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(faceDir.x, 0, faceDir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, range);
  
    }


    IEnumerator AttackPerSecond(float DPS)
    {

        while (true)
        {
            while (animator!=null && canAttack)
            {
                if (isDead) { break; }
                animator.SetBool("attack", true);
                animator.SetBool("moving", false);
                target.GetComponent<Player>().Health = damage;
                yield return new WaitForSeconds(DPS);
            }




            yield return null;
        }



    }


    public void OnDamageTaken()
    {
        isProvoked = true;
        moveObjective = target.transform.position;
    }
}
