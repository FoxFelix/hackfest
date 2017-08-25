using UnityEngine;
using System.Collections;
using EasyInputVR.Core;
using UnityEngine.Events;
using System;

namespace EasyInputVR.StandardControllers
{

    [AddComponentMenu("EasyInputGearVR/Standard Controllers/StandardTeleportReceiver")]
    public class StandardTeleportReceiver : StandardBaseReceiver
    {

        public EasyInputConstants.ACTION_CONDITION teleportCondition = EasyInputConstants.ACTION_CONDITION.GearVRTriggerPressed;
        public GameObject teleportObject;
        public float yAxisOffset = 0f;
        public float timeLockout = 2f;

        bool fireEvent;
        bool clicking;
        float lastTeleport = 0f;
        Vector2 touchPos;


        void OnEnable()
        {


            if (teleportCondition == EasyInputConstants.ACTION_CONDITION.TouchpadTouched)
            {
                EasyInputHelper.On_Touch += localTouch;
                EasyInputHelper.On_TouchEnd += localTouchEnd;
            }
            else
            {
                EasyInputHelper.On_ClickEnd += clickEnd;
                EasyInputHelper.On_Click += click;
            }

        }

        void OnDestroy()
        {
            if (teleportCondition == EasyInputConstants.ACTION_CONDITION.TouchpadTouched)
            {
                EasyInputHelper.On_Touch -= localTouch;
                EasyInputHelper.On_TouchEnd -= localTouchEnd;
            }
            else
            {
                EasyInputHelper.On_ClickEnd -= clickEnd;
                EasyInputHelper.On_Click -= click;
            }
        }


        // Update is called once per frame
        void Update()
        {
        }


        public override void Hover(Vector3 hitPosition)
        {
            if (conditionsMet() && (Time.time - lastTeleport) > timeLockout)
            {
                //teleport
                lastTeleport = Time.time;
                hitPosition.y += yAxisOffset;
                teleportObject.transform.position = hitPosition;
            }
        }

        public override void HoverEnter(Vector3 hitPosition)
        {

        }

        public override void HoverExit(Vector3 hitPosition)
        {
        }

        void localTouch(InputTouch touch)
        {
            touchPos = touch.currentTouchPosition;
        }

        void localTouchEnd(InputTouch touch)
        {
            touchPos = EasyInputConstants.NOT_TOUCHING;
        }


        void clickEnd(ButtonClick button)
        {
            if ((int)button.button == (int)teleportCondition)
                clicking = false;
        }

        void click(ButtonClick button)
        {
            if ((int)button.button == (int)teleportCondition)
                clicking = true;
        }

        bool conditionsMet()
        {
            fireEvent = false;
            if (teleportCondition == EasyInputConstants.ACTION_CONDITION.TouchpadTouched && !touchPos.Equals(EasyInputConstants.NOT_TOUCHING))
                fireEvent = true;
            else if (clicking)
                fireEvent = true;


            return fireEvent;


        }
    }

}
