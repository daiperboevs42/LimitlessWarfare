using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankTry
{

    public class Tank_Inputs : MonoBehaviour
    {
        #region Variables
        [Header("Input Properties")]
        public new Camera camera;
        #endregion

        #region Properties

        private Vector3 pointerPosition;
        public Vector3 PointerPosition
        {
            get { return pointerPosition; }
        }

        private Vector3 pointerNormal;
        public Vector3 PointerNormal
        {
            get { return pointerNormal; }
        }

        private float forwardInput;
        public float ForwardInput
        {
            get { return forwardInput; }
        }

        private float rotationInput;
        public float RotationInput
        {
            get { return rotationInput; }
        }
        #endregion

        #region Builtin Methods
        void Update()
        {
            if (camera)
            {
                HandleInputs();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pointerPosition, 0.5f);
        }
        #endregion

        #region Custom Methods
        // protected - no other class outside of this class can access the method unless it inherits from this Script
        protected virtual void HandleInputs()
        {
            Ray screenRay = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(screenRay, out hit))
            {
                // explaination needed
                pointerPosition = hit.point;
                pointerNormal = hit.normal;
            }
            forwardInput = Input.GetAxis("Vertical1");
            rotationInput = Input.GetAxis("Horizontal1");
        }
        #endregion
    }
}
