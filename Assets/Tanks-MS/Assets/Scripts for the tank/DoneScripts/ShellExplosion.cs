using UnityEngine;

namespace LimitlessWarfare
{
    public class ShellExplosion : MonoBehaviour
    {
        #region Variables
        [Header("Layer Properties")]
        public LayerMask m_TankMask;                        // Used to filter what the explosion affects.

        [Header("Audio Properties")]
        public ParticleSystem m_ExplosionParticles;         // Reference to the particles that will play on explosion.
        public AudioSource m_ExplosionAudio;                // Reference to the audio that will play on explosion.
        public AudioClip m_explosionClip;

        [Header("ShellExplosion Properties")]
        public float m_MaxDamage = 50f;                    // The amount of damage done if the explosion is centred on a tank.
        public float m_ExplosionForce = 10f;              // The amount of force added to a tank at the centre of the explosion.
        public float m_MaxLifeTime = 2f;                    // The time in seconds before the shell is removed.
        public float m_ExplosionRadius = 2.5f;                // The maximum distance away from the explosion tanks can be and are still affected.
        #endregion

        #region Custom Methods
        private void OnTriggerEnter (Collider other)
        {

            //if(other.name != "Ground") { return; }
            // Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius.
            //Will create an imaginary sphere and everythinhg that overlaps that spehere or is inside the sphere is going to be collected.
            // using tank mask sp it will only pickup the tanks and not other things.
            Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);

            // Go through all the colliders...
            for (int i = 0; i < colliders.Length; i++)
            {

                // ... and find their rigidbody.
                Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

                // If they don't have a rigidbody, go on to the next collider.
                if (!targetRigidbody)
                    continue;

                // Add an explosion force.
                //targetRigidbody.AddExplosionForce (m_ExplosionForce, transform.position, m_ExplosionRadius);

                // Find the TankHealth script associated with the rigidbody
                //colliders[i].gameObject.GetComponent<TankHealth>().TakeDamage(15);
                TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

                // If there is no TankHealth script attached to the gameobject, go on to the next collider.
                if (!targetHealth)
                    continue;

                // Calculate the amount of damage the target should take based on it's distance from the shell.
                float damage = CalculateDamage(targetRigidbody.position);

                // Deal this damage to the tank.
                targetHealth.TakeDamage(damage);
            }

            // Unparent the particles from the shell. When shell explode we want to move it from the scene. 
            //If removed the particles and audio also will be removed because they are children of parent, then we "un-Parent" them so that they still will play
            //when the shell is destroyed.
            //the children will then, when the shell explodes, decouple themself from the parent and the explosion will no longer be part of the shell.
            m_ExplosionParticles.transform.parent = null;

            
 
            // Play the particle system.
            m_ExplosionParticles.Play();

            Debug.Log(m_ExplosionAudio.clip);

            // try this link https://stackoverflow.com/questions/42356523/unity-audiosource-unable-to-play-clip

           m_ExplosionAudio.clip = m_explosionClip;
            m_ExplosionAudio.Play();






            // Once the particles have finished, destroy the gameobject they are on.
            ParticleSystem.MainModule mainModule = m_ExplosionParticles.main;
            Destroy(m_ExplosionParticles.gameObject, mainModule.duration);




            // Destroy the shell.
            Destroy(gameObject);


        }

        private float CalculateDamage (Vector3 targetPosition)
        {
            // Create a vector from the shell to the target.
            Vector3 explosionToTarget = targetPosition - transform.position;

            // Calculate the distance from the shell to the target.
            float explosionDistance = explosionToTarget.magnitude;

            // Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
            float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

            // Calculate damage as this proportion of the maximum possible damage.
            float damage = relativeDistance * m_MaxDamage;

            // Make sure that the minimum damage is always 0.
            damage = Mathf.Max (0f, damage);

            return damage;
        }
    }
    #endregion
}