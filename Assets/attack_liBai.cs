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
            if (triggerPressed == true || Input.GetKeyDown(KeyCode.F)) 
            {
                attack();
            }
        }
    }

    
    void attack()
    {
        pv.RPC("fxAttackLiBai", PhotonTargets.All);
        RaycastHit hit;
        if (Physics.Raycast(handPointer.position, handPointer.forward, out hit, 10f))
        {
            if (hit.transform.gameObject.layer == 10)
            {

            }
        }
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
