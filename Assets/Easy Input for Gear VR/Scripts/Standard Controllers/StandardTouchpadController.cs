using UnityEngine;
using System.Collections;
using EasyInputVR.Core;
using System;

namespace EasyInputVR.StandardControllers
{

    [AddComponentMenu("EasyInputGearVR/Standard Controllers/StandardTouchpadController")]
    public class StandardTouchpadController : MonoBehaviour
    {
        public EasyInputConstants.AXIS axisHorizontal = EasyInputConstants.AXIS.XAxis;
        public EasyInputConstants.AXIS axisVertical = EasyInputConstants.AXIS.YAxis;
        public EasyInputConstants.ACTION_TYPE action = EasyInputConstants.ACTION_TYPE.Position;
        public float sensitivity = 1f;

        //runtime variables
        Vector2 lastFrameTouch = EasyInputConstants.NOT_TOUCHING;
        Vector3 actionVector3;
        float horizontal;
        float vertical;


        void OnEnable()
        {
            EasyInputHelper.On_Touch += localAxis;
            EasyInputHelper.On_TouchEnd += localAxisEnd;
        }

        void OnDestroy()
        {
            EasyInputHelper.On_Touch -= localAxis;
            EasyInputHelper.On_TouchEnd -= localAxisEnd;
        }

        // Update is called once per frame
        void Update()
        {
        }

        void localAxisEnd(InputTouch touch)
        {
            //we just ended a touch so reset the last frame
            lastFrameTouch = EasyInputConstants.NOT_TOUCHING;
        }

        void localAxis(InputTouch touch)
        {
            //first check to see if this is the first frame
            if (lastFrameTouch == EasyInputConstants.NOT_TOUCHING)
            {
                lastFrameTouch = touch.currentTouchPosition;
                return;
            }

            //otherwise is a continuation
            horizontal = (touch.currentTouchPosition.x - lastFrameTouch.x) * sensitivity * Time.deltaTime * 100f;
            vertical = (touch.currentTouchPosition.y - lastFrameTouch.y) * sensitivity * Time.deltaTime * 100f;
            actionVector3 = EasyInputUtilities.getControllerVector3(horizontal, vertical, axisHorizontal, axisVertical);

            switch (action)
            {
                case EasyInputConstants.ACTION_TYPE.Position:
                    transform.position += actionVector3;
                    break;
                case EasyInputConstants.ACTION_TYPE.Rotation:
                    transform.Rotate(actionVector3, Space.World);
                    break;
                case EasyInputConstants.ACTION_TYPE.LocalPosition:
                    transform.Translate(actionVector3);
                    break;
                case EasyInputConstants.ACTION_TYPE.LocalRotation:
                    transform.Rotate(actionVector3);
                    break;
                case EasyInputConstants.ACTION_TYPE.LocalScale:
                    transform.localScale += actionVector3;
                    break;
                default:
                    Debug.Log("Invalid Action");
                    break;

            }

            lastFrameTouch = touch.currentTouchPosition;
        }


    }

}

