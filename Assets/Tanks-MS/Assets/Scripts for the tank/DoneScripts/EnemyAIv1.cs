using UnityEngine;
namespace EndTank
{
    public class EnemyAIv1 : EnemyMovement
    {
        public float speed;
        public float stoppingDistance;
        public float retreatDistance;

        private float timeBtwShots;
        public float startTimeBtwShots;

        public GameObject projectile;

        private Transform player;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;

            timeBtwShots = startTimeBtwShots;
        }

        // Update is called once per frame
        void Update()
        {
            //checks if player position is further away than stoppingDistance
            if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
            {
                //moves towards the player
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            //checks if player is closer than stopping distance and not within retreat distance
            else if (Vector3.Distance(transform.position, player.position) < stoppingDistance
                && Vector3.Distance(transform.position, player.position) > retreatDistance)
            {
                //stops moving if in good distance from player
                transform.position = this.transform.position;
            }
            //checks if player is closer than stopping distance but within retreat distance
            else if (Vector3.Distance(transform.position, player.position) > retreatDistance)
            {
                //moves away from player if they're too close
                transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }

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
}
