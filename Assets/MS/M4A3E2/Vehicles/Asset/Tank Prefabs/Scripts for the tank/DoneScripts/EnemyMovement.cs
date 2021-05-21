using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EndTank
{
    [RequireComponent(typeof(Rigidbody))]

    public class EnemyMovement : MonoBehaviour
    {
        #region Variables
        // public int m_PlayerNumber = 1;
        [Header("Movement Properties")]
        public float m_Speed = 12f;
        public float m_TurnSpeed = 180f;

        [Header("Audio Properties")]
        public AudioSource m_MovementAudio;         // Hvilken audio source der er brugt til, at afspille motor sounds..
        public AudioClip m_EngineIdling;            // Audio når tanken står stille
        public AudioClip m_EngineDriving;           // Audio når tanken bevæger sig
        public float m_PitchRange = 0.2f;           // Forskel på pitch for, at det ikke kommer til at larme når flere tanks kører på samme tid 

        private Rigidbody m_Rigidbody;
        private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.
        private ParticleSystem[] m_particleSystems; // alle particles systems til Tanks
        #endregion

        #region Builtin Methods
        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();

        }

        // Start is called before the first frame update
        void Start()
        {

            m_OriginalPitch = m_MovementAudio.pitch;
        }

        // Update is called once per frame
        void Update()
        {
            EngineAudio();
        }
        #endregion

        #region Custom Methods

        private void EngineAudio()
        {


            // ... and if the audio source is currently playing the driving clip...
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                // ... change the clip to idling and play it.
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }

            else
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
        private void Move()
        {
            // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
            Vector3 movement = transform.forward * m_Speed * Time.deltaTime;

            // Apply this movement to the rigidbody's position.
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);


        }
        private void Turn()
        {
            // Determine the number of degrees to be turned based on the input, speed and time between frames.
            float turn = m_TurnSpeed * Time.deltaTime;

            // Make this into a rotation in the y axis.
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

            // Apply this rotation to the rigidbody's rotation.
            m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);

        }
        private void OnDisable()
        {
            // When the tank is turned off, set it to kinematic so it stops moving.
            m_Rigidbody.isKinematic = true;

            // Stop all particle system so it "reset" it's position to the actual one instead of thinking we moved when spawning
            for (int i = 0; i < m_particleSystems.Length; ++i)
            {
                m_particleSystems[i].Stop();
            }
        }
        private void OnEnable()
        {
            // When the tank is turned on, make sure it's not kinematic.
            // Kinematic means the no forces will be applied and its set to false else it would be trouble moving it
            m_Rigidbody.isKinematic = false;

            // grab all the Particle systems children of that Tank to be able to Stop/Play them on Deactivate/Activate
            // It is needed because we move the Tank when spawning it, and if the Particle System is playing while we do that
            // it "think" it move from (0,0,0) to the spawn point, creating a huge trail of smoke
            m_particleSystems = GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < m_particleSystems.Length; ++i)
            {
                m_particleSystems[i].Play();
            }
        }
        #endregion

    }
}