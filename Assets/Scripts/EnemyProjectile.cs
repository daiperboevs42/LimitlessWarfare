using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        //gets player and last player location
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector3(player.position.x, player.position.y, player.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //moves projectile towards the location of the player of the time the projectile was shot 
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        //checks if projectile has hit target
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyProjectile();
        }
        else if(other.CompareTag("Obstacle"))
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        //removes projectile when it reaches target
        Destroy(gameObject);
    }
}
