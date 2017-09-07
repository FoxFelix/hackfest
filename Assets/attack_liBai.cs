using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyInputVR.Core;

public class attack_liBai : MonoBehaviour
{
    PhotonView pv;
    Vector3 myOrientation = Vector3.zero;
    bool triggerPressed;
    public Transform handPointer;
    bool readyToShoot = true;
    bool canShoot = true;
    bool firstTime = true;

    bool throwInProcess = false;
    bool preventThrowAgain = false;
    Vector3 temp;
    Vector3 currentThrow = new Vector3(0f, 0f, 0f);
    float currentThrowForce = 0;
    Vector3 currentTorque = new UnityEngine.Vector3(0f, 0f, 0f);
    //Vector3 testThrow = new Vector3(0f, 0f, 450f);
    //Vector3 testTorque = new Vector3(0f, 0f, 0f);
    Rigidbody throwObjectRigidBody;
    float degreesOff = 0f;
    PhysicMaterial myPhysics;

    void OnEnable()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
            EasyInputHelper.On_Motion += localMotion;
#endif
        EasyInputHelper.On_ClickStart += localClickStart;
        EasyInputHelper.On_ClickEnd += localClickEnd;
        //EasyInputHelper.On_ClickStart += initiateThrow;
        //EasyInputHelper.On_ClickEnd += endThrow;
        //EasyInputHelper.On_Motion += trackThrow;
    }

    void OnDestroy()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
            EasyInputHelper.On_Motion -= localMotion;
#endif
        EasyInputHelper.On_Click -= localClickStart;
        EasyInputHelper.On_Click -= localClickEnd;
        //EasyInputHelper.On_ClickStart -= initiateThrow;
        //EasyInputHelper.On_ClickEnd -= endThrow;
        //EasyInputHelper.On_Motion -= trackThrow;
    }


    //void initiateThrow(ButtonClick click)
    //{
    //    if (click.button == EasyInputConstants.CONTROLLER_BUTTON.GearVRTrigger && !preventThrowAgain)
    //    {
    //        //reset the motion variables
    //        EasyInputHelper.resetMotion();
    //        throwInProcess = true;
    //        //follow.stopFollow();
    //    }
    //}

    //void endThrow(ButtonClick click)
    //{
    //    if (click.button == EasyInputConstants.CONTROLLER_BUTTON.GearVRTrigger && !preventThrowAgain)
    //    {

    //        //reset the motion variables
    //        EasyInputHelper.resetMotion();
    //        throwInProcess = false;
    //        //preventThrowAgain = true;
    //        //temp = transform.position;
    //        //temp.y = .5f;
    //        //transform.position = temp;

    //        //here we do the throw after some sanity checks
    //        throwThings(currentThrowForce);
            
    //        //the sudden drop of the ball makes it want to bounce off the alley prevent this in the rigidbody
    //        //temp = throwObjectRigidBody.velocity;
    //        //temp.y = 0f;
    //        //throwObjectRigidBody.velocity = temp;

    //        //start clock to reset the scene
    //        //Invoke("resetScene", 8f);
    //    }
    //}

    void throwThings(float force)
    {
        pv.RPC("fxThrow", PhotonTargets.All, force);
    }
    void trackThrow(EasyInputVR.Core.Motion motion)
    {
        //get into a -180 to 180 range
        motion.currentOrientationEuler.z = (motion.currentOrientationEuler.z > 180f) ? (motion.currentOrientationEuler.z - 360f) : motion.currentOrientationEuler.z;

        if (throwInProcess)
        {
            //there are a couple of factors that you see in a bowling motion if you look at the telemetry
            //left or right handed can be determined from the x axis user acceleration during the backswing (doesn't matter in this demo but if we had animation of a player could be used for that)
            //hardness of throw can use the user acceleration z axis during the forward swing (higher value harder throw)
            //spin can be determined from the x and z axis during forward part of the throw

            //aim of shot is the hardest as some drift will have already occured (the aim is really only dictated by
            //the direction going the last few frames before release of the button) best place to gauge this is the velocity
            //vector but there will have been some drift and in tests the sensors on board aren't good enough
            //so instead user will actually pick aim before throw

            //base hardness off current velocity.z (light throw tends to be around 1.0 and really hard around 3.0)
            //in terms of the game the lightest throw should be 450 and the hardest be 700
            if (EasyInputHelper.isGearVR)
            {
                if (motion.totalVelSinceLastReset.z < -5f)
                {
                    //currentThrow.z = 200f + (10f * -motion.totalVelSinceLastReset.z);
                    currentThrowForce = ( -motion.totalVelSinceLastReset.z);
                }
                else
                {
                    //we hit and released the button but we didn't really do the motion
                    //currentThrow.z = 30f;
                    currentThrowForce = 3;
                }
            }
            else
            {
                //we hit and released the button but we didn't really do the motion
                //currentThrow.z = 30f;
                currentThrowForce = 3;
            }

            //calculate the x component off the previously detemined aim (degreesOff)
            //currentThrow.x = degreesOff * (currentThrow.z / 100);

            //base spin off of angular velocity

            //if (motion.totalAngularVelSinceLastReset.z < 20f && motion.totalAngularVelSinceLastReset.z > -20f)
            //{
            //    currentTorque.z = 0f;
            //}
            //else
            //{
            //    if (EasyInputHelper.isGearVR)
            //        currentTorque.z = -motion.totalAngularVelSinceLastReset.z / 8f;
            //}


        }
    }


    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (pv.isMine == true)
        {
            if (triggerPressed == true || Input.GetKeyDown(KeyCode.F)) 
            {
                attack();
            }
            if (triggerPressed == false)
            {
                readyToShoot = true;
                firstTime = true;
            }
            //if (throwInProcess)
            //{
            //    temp = transform.position;
            //    temp.y = 1.5f;
            //    //transform.position = temp;
            //    //throwObjectRigidBody.velocity = Vector3.zero;
            //}
            //else
            //{
            //    if (transform.position.z > 0)
            //    {
            //        myPhysics.dynamicFriction = transform.position.z / 20f;
            //        myPhysics.staticFriction = transform.position.z / 20f;
            //    }
            //}

        }
    }

    
    void attack()
    {
        if (firstTime == true && canShoot == true)
        {
            pv.RPC("fxAttackLiBai", PhotonTargets.All);
            firstTime = false;
            canShoot = false;
            Invoke("resetCanShoot", 2.2f);
        }

        //RaycastHit hit;
        //if (Physics.Raycast(handPointer.position, handPointer.forward, out hit, 10f))
        //{
        //    if (hit.transform.gameObject.layer == 10 && readyToShoot == true && canShoot == true)
        //    {
        //        pv.RPC("fxAttackLiBai", PhotonTargets.All);
        //        readyToShoot = false;
        //        canShoot = false;
        //        Invoke("resetCanShoot", 3);
        //    }
        //}
    }

    void resetCanShoot()
    {
        canShoot = true;
    }


    void localMotion(EasyInputVR.Core.Motion motion)
    {
        myOrientation = motion.currentOrientationEuler;
    }

    void localClickStart(ButtonClick button)
    {

        if (button.button == EasyInputConstants.CONTROLLER_BUTTON.GearVRTrigger)
        {
            triggerPressed = true;
        }
    }

    void localClickEnd(ButtonClick button)
    {

        if (button.button == EasyInputConstants.CONTROLLER_BUTTON.GearVRTrigger)
        {
            triggerPressed = false;
        }
    }

}
