using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] float range;
    [SerializeField]int damage;
    [Range(0.1f,3f)][SerializeField] float damageTimer;
    [SerializeField] int maxHealth;


    NavMeshAgent navMeshAgent;
    float distance = Mathf.Infinity;

    bool isProvoked;
    bool canAttack;
    private int _health;

    public int Health
    {
        get { return _health; }
        set { _health -= value;
            StartDeath();

              }
        
    }


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(AttackPerSecond(damageTimer));
        _health = maxHealth;

    }


    void StartDeath()
    {
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, target.position);



        if (isProvoked)
        {
            EngageTarget();
        }else if(distance < range)
        {
            isProvoked = true;
        }

        
    }

    private void EngageTarget()
    {
        Chase();

    }

    private void Chase()
    {
        navMeshAgent.SetDestination(target.position);

        if (distance < navMeshAgent.stoppingDistance)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
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
            while (canAttack)
            {
                target.GetComponent<Player>().Health = damage;
                yield return new WaitForSeconds(DPS);
            }



            yield return null;
        }



    }
}
