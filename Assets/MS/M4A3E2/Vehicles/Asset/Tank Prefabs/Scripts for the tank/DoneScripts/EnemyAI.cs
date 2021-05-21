using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace EndTank
{
    public class EnemyAI : MonoBehaviour
    {
        NavMeshAgent navMeshAgent;
        public float speed;
        private float timeBtwShots;
        public float startTimeBtwShots;
       // public GameObject projectile;
        public GameObject player;
        private bool isDetected = false;
        public float sightRange;


        public Rigidbody m_Shell;
        public float m_launchForce = 20.0f;

        [Header("Turret Properties")]
        public Transform turretTransform;
        public float turretLagSpeed = 4f;
        private Vector3 finalTurretPointDir;
        public Transform barrelEnd;

        [Header("Pointer Properties")]
        public Transform pointerTransform;

        [Header("Audio Properties")]
        public AudioSource m_MovementAudio;         // Hvilken audio source der er brugt til, at afspille motor sounds..
        public AudioClip m_EngineIdling;            // Audio når tanken står stille
        public AudioClip m_EngineDriving;           // Audio når tanken bevæger sig
        public float m_PitchRange = 0.2f;           // Forskel på pitch for, at det ikke kommer til at larme når flere tanks kører på samme tid 
        private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.

        [Header("Audio Properties")]
        public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.

        public AudioClip m_FireClip;

        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = this.GetComponent<NavMeshAgent>();
            navMeshAgent.speed = speed;
            //player = GameObject.FindGameObjectWithTag("Player");
            timeBtwShots = startTimeBtwShots;
            m_OriginalPitch = m_MovementAudio.pitch;
        }

        void SetDestination()
        {
            Vector3 targetVector = player.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }

        void DetectEnemy()
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);


            if (dist < sightRange)
                isDetected = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isDetected)
                DetectEnemy();
            else if (isDetected)
            {
                SetDestination();
                HandleTurret();
                if (timeBtwShots <= 0)
                {
                    Shoot();

                    //ensures that enemy doesnt shoot every frame
                    timeBtwShots = startTimeBtwShots;
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }
            }
            EngineAudio();
        }

        protected virtual void HandleTurret()
        {
            if (turretTransform)
            {
                Vector3 targetVector = player.transform.position;
                Vector3 turretPointDirect = targetVector - turretTransform.position;
                turretPointDirect.y = 0f;

                finalTurretPointDir = Vector3.Lerp(finalTurretPointDir, turretPointDirect, Time.deltaTime * turretLagSpeed);
                turretTransform.rotation = Quaternion.LookRotation(finalTurretPointDir);


            }
        }

        private void Shoot()
        {
            // Create an instance of the shell and store a reference to it's rigidbody.
            Rigidbody shellInstance =
            //    Instantiate (m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
                            Instantiate(m_Shell, barrelEnd.position, barrelEnd.rotation) as Rigidbody;


            // Set the shell's velocity to the launch force in the fire position's forward direction.
            shellInstance.velocity = barrelEnd.transform.TransformDirection(Vector3.forward * m_launchForce);

            // Change the clip to the firing clip and play it.
            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play();
        }

        private void EngineAudio()
        {
            // If there is no input (the tank is stationary)...
            if (!isDetected)
            {
                // ... and if the audio source is currently playing the driving clip...
                if (m_MovementAudio.clip == m_EngineDriving)
                {
                    // ... change the clip to idling and play it.
                    m_MovementAudio.clip = m_EngineIdling;
                    m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                    m_MovementAudio.Play();
                }
            }
            else if(isDetected)
            {
                // Otherwise if the tank is moving and if the idling clip is currently playing...
                if (m_MovementAudio.clip == m_EngineIdling)
                {
                    // ... change the clip to driving and play.
                    m_MovementAudio.clip = m_EngineDriving;
                    m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                    m_MovementAudio.Play();
                }
            }
        }
    }
    
}