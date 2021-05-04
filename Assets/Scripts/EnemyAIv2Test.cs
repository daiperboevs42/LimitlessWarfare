using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIv2Test : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    public float speed;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject projectile;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
    }

    void SetDestination()
    {
            Vector3 targetVector = player.transform.position;
            navMeshAgent.SetDestination(targetVector);
    }

    // Update is called once per frame
    void Update()
    {
        SetDestination();

        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            //Quaternion == no rotation basically  ¯\_(ツ)_/¯

            //ensures that enemy doesnt shoot every frame
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
