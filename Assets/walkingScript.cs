using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyInputVR.Core;

public class walkingScript : MonoBehaviour
{
    //public Camera cam;
    public Transform directionTransform;
    public Transform moveTarget;
    public float speed;
    public Rigidbody rb;
    PhotonView pv;

    Vector3 myOrientation = Vector3.zero;
    bool gasPressed;
    bool brakePressed;
    float horizontal, vertical;
    float normalizeDegrees = 90f;
    float sensitivity = 1f;
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
        pv = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (pv.isMine == true)
        {
            //if (myOrientation != Vector3.zero)
            //{
            //    vertical = myOrientation.x;
            //    //get into a -180 to 180 range
            //    vertical = (vertical > 180f) ? (vertical - 360f) : vertical;
            //}

            //Quaternion camRot = cam.transform.localRotation;
            //camRot.x = 0;
            //camRot.z = 0;
            //directionTransform.localRotation = camRot;

            if (gasPressed || Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(directionTransform.forward * speed);
                walkSideways(myOrientation);
            }
        }
    }

    void walk()
    {

    }

    public void walkSideways(Vector3 myOrientation)
    {


        if (myOrientation != Vector3.zero)
        {
            horizontal = myOrientation.z;

            //get into a -180 to 180 range
            horizontal = (horizontal > 180f) ? (horizontal - 360f) : horizontal;
            horizontal = horizontal / normalizeDegrees;
            horizontal *= -sensitivity;
        }
        else
        {
            horizontal = 0f;
        }

        rb.AddForce(directionTransform.right * horizontal * 200);
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
