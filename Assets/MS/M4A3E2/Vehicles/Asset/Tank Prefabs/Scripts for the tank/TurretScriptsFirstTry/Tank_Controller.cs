using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankTry
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Tank_Inputs))]
    public class Tank_Controller : MonoBehaviour
    {

        #region Variables
        [Header("Movement Properties")]
        public float tankSpeed = 15f;
        public float tankRotationSpeed = 20f;

        [Header("Turret Properties")]
        public Transform turretTransform;
        public float turretLagSpeed = 4f;


        [Header("Pointer Properties")]
        public Transform pointerTransform;

       
        private Rigidbody rb;
        private Tank_Inputs input;
        private Vector3 finalTurretPointDir;
        #endregion

        #region Builtin Methods
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            input = GetComponent<Tank_Inputs>();
        }

        // whenever you are dealing with a ridgidbody, you need to use the FixedUpdate().
        // Its called at a different rate and physics need that fixed rate to work properly otherwise the physics will go crazy.
        void FixedUpdate()
        {
            if (rb && input)
            {
                HandleMovement();
                HandleTurret();
                HandlePointer();
            }
        }

       
        #endregion

        #region Custom Methods
        // protected - no other class outside of this class can access the method unless it inherits from this Script
        // we do this to be able to override any of these methods. Like if you only need the base of the script.
        protected virtual void HandleMovement()
        {
            //move forward
            Vector3 wantedPosition = transform.position + (transform.forward * input.ForwardInput * tankSpeed * Time.deltaTime);
            rb.MovePosition(wantedPosition);

            //Rotation
            Quaternion wantedRotation = transform.rotation * Quaternion.Euler(Vector3.up * (tankRotationSpeed * input.RotationInput * Time.deltaTime));
            rb.MoveRotation(wantedRotation);
         }

        protected virtual void HandleTurret()
        {
            if (turretTransform)
            {
                Vector3 turretPointDirect = input.PointerPosition - turretTransform.position;
                turretPointDirect.y = 0f;

                finalTurretPointDir = Vector3.Lerp(finalTurretPointDir, turretPointDirect, Time.deltaTime * turretLagSpeed);
                turretTransform.rotation = Quaternion.LookRotation(finalTurretPointDir);
            }
        }

        protected virtual void HandlePointer()
        {
            if (pointerTransform)
            {
                pointerTransform.position = input.PointerPosition;
            }
        }

        #endregion
    }
}
