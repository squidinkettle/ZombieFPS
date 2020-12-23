using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class TransportCar : MonoBehaviour
{
    [SerializeField] GameObject enemyAi;
    [SerializeField] int numberOfEnemies;
    [SerializeField] Transform player;
    NavMeshAgent navMeshAgent;
    Queue<GameObject> enemiesInsideTransport;
    bool enemiesLeave;
    [SerializeField]float timeEnemiesLeave=1f;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        InitializeQueue();
        StartCoroutine(EnemiesLeaveVehicle(timeEnemiesLeave));
    }

    private void InitializeQueue()
    {
        enemiesInsideTransport = new Queue<GameObject>();

        for (int x = 0; x < numberOfEnemies; x++)
        {
            enemiesInsideTransport.Enqueue(enemyAi);
        }
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(player.position);

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < navMeshAgent.stoppingDistance)
        {
            enemiesLeave=true;
        }

    }

    IEnumerator EnemiesLeaveVehicle(float timeLeave)
    {
        while (true)
        {
            while(enemiesLeave && enemiesInsideTransport.Count >0)
            {
                float xOffset = 1f;
                Vector3 offLoadPos = new Vector3(
                transform.position.x + xOffset,
                transform.position.y,
                transform.position.z);
                var newEnemy = enemiesInsideTransport.Dequeue();
                Instantiate(newEnemy, offLoadPos,Quaternion.identity);


                yield return new WaitForSeconds(timeLeave);
            }



            yield return null;
        }
    }
}
