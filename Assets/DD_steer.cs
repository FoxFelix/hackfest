using UnityEngine;
using System;
using System.Collections;
using EasyInputVR.Core;

    [AddComponentMenu("EasyInputGearVR/Miscellaneous/Steering")]
    public class DD_steer : MonoBehaviour
    {

        Rigidbody myRigidbody;
        Vector3 myOrientation = Vector3.zero;
        bool gasPressed;
        bool brakePressed;
        float horizontal, vertical;
        float normalizeDegrees = 90f;
        float sensitivity = 4f;
        Vector3 actionVectorPosition;
        Vector3 computerVector;
        public Transform mesh;

        void OnEnable()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            EasyInputHelper.On_Motion += localMotion;
#endif
            EasyInputHelper.On_ClickStart += localClickStart;
            EasyInputHelper.On_ClickEnd += localClickEnd;

        }

        void OnDestroy()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            EasyInputHelper.On_Motion -= localMotion;
#endif
            EasyInputHelper.On_Click -= localClickStart;
            EasyInputHelper.On_Click -= localClickEnd;
        }

        void Start()
        {
            myRigidbody = this.GetComponent<Rigidbody>();
        }

        void Update()
        {
            //gas
            if (gasPressed)
            {
                if (myRigidbody.velocity.magnitude > 1f)
                    myRigidbody.AddForce(transform.forward * sensitivity * 2f);
                else
                    myRigidbody.AddForce((transform.forward) * sensitivity * 30f);
            }

            //brake
            if (brakePressed)
            {
                if (myRigidbody.velocity.magnitude > 1f)
                    myRigidbody.AddForce(-transform.forward);
                else
                    myRigidbody.AddForce(-transform.forward * 10f);
            }

            //steering
            steerBall(myOrientation);
        }


        public void steerBall(Vector3 myOrientation)
        {


            if (myOrientation != Vector3.zero)
            {
                horizontal = myOrientation.z;
                vertical = myOrientation.x;

                //get into a -180 to 180 range
                horizontal = (horizontal > 180f) ? (horizontal - 360f) : horizontal;
                vertical = (vertical > 180f) ? (vertical - 360f) : vertical;
                //if (horizontal > -90 && horizontal < 90)
                //{
                //    var tiltH = horizontal;
                //    var tiltV = vertical;
                //    var newRot = Quaternion.Euler(tiltV, 0, tiltH);
                //    mesh.localRotation = Quaternion.Slerp(mesh.localRotation, newRot, 0.5f);
                //}




                horizontal = horizontal / normalizeDegrees;
                vertical = vertical / normalizeDegrees;

                horizontal *= -sensitivity;
                vertical *= sensitivity;
            }
            else
            {
                horizontal = 0f;
                vertical = 0f;
            }

            //actionVectorPosition.x = horizontal;
            actionVectorPosition.x = 0f;
            actionVectorPosition.z = 0f;
            actionVectorPosition.y = -vertical;

            //myRigidbody.AddForce(actionVectorPosition);
            if (horizontal <= 5 && horizontal >= -5)
                myRigidbody.AddTorque(0, horizontal *0.1f, 0);
            if (horizontal > 5)
                myRigidbody.AddTorque(0, 5 * 0.1f, 0);
            if (horizontal < -5)
                myRigidbody.AddTorque(0, -5 * 0.1f, 0);

        }

        void localMotion(EasyInputVR.Core.Motion motion)
        {
            myOrientation = motion.currentOrientationEuler;
        }

        void localClickStart(ButtonClick button)
        {
            //if (button.button == EasyInputConstants.CONTROLLER_BUTTON.GearVRTouchClick)
            //{
            //    brakePressed = true;
            //}
            if (button.button == EasyInputConstants.CONTROLLER_BUTTON.GearVRTouchClick)
            {
                gasPressed = true;
            }
        }

        void localClickEnd(ButtonClick button)
        {
            //if (button.button == EasyInputConstants.CONTROLLER_BUTTON.GearVRTouchClick)
            //{
            //    brakePressed = false;
            //}
            if (button.button == EasyInputConstants.CONTROLLER_BUTTON.GearVRTouchClick)
            {
                gasPressed = false;
            }
        }

    }