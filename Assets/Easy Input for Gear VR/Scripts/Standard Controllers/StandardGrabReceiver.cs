using UnityEngine;
using System.Collections;
using EasyInputVR.Core;
using UnityEngine.Events;

namespace EasyInputVR.StandardControllers
{

    [AddComponentMenu("EasyInputGearVR/Standard Controllers/StandardGrabReceiver")]
    public class StandardGrabReceiver : StandardBaseReceiver
    {

        public EasyInputConstants.ACTION_CONDITION grabCondtion = EasyInputConstants.ACTION_CONDITION.GearVRTriggerPressed;
        public bool lockXAxis;
        public bool lockYAxis;
        public bool lockZAxis;

        Vector3 currentFrameHit = EasyInputConstants.NOT_VALID;
        Vector3 lastFrameHit = EasyInputConstants.NOT_VALID;
        Vector3 delta = EasyInputConstants.NOT_VALID;
        bool fireEvent;
        bool clicking;
        bool grabMode;
        Vector2 touchPos;


        void OnEnable()
        {
            if (grabCondtion == EasyInputConstants.ACTION_CONDITION.TouchpadTouched)
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
            if (grabCondtion == EasyInputConstants.ACTION_CONDITION.TouchpadTouched)
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

        void LateUpdate()
        {
            if (grabMode && conditionsMet() && lastFrameHit != EasyInputConstants.NOT_VALID)
            {
                delta = currentFrameHit - lastFrameHit;
            }
            else if (!conditionsMet() || !grabMode)
            {
                grabMode = false;
                delta = Vector3.zero;
            }

            if (lockXAxis)
                delta.x = 0f;
            if (lockYAxis)
                delta.y = 0f;
            if (lockZAxis)
                delta.z = 0f;

            this.transform.position += delta;

            //processing done
            lastFrameHit = currentFrameHit;
        }

        public override void Hover(Vector3 hitPosition)
        {
            if (conditionsMet())
            {
                grabMode = true;
                currentFrameHit = hitPosition;
            }
        }

        public override void HoverEnter(Vector3 hitPosition)
        {
        }

        public override void HoverExit(Vector3 hitPosition)
        {
            grabMode = false;
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
            if ((int)button.button == (int)grabCondtion)
                clicking = false;
        }

        void click(ButtonClick button)
        {
            if ((int)button.button == (int)grabCondtion)
                clicking = true;
        }

        bool conditionsMet()
        {
            fireEvent = false;
            if (grabCondtion == EasyInputConstants.ACTION_CONDITION.TouchpadTouched && !touchPos.Equals(EasyInputConstants.NOT_TOUCHING))
                fireEvent = true;
            else if (clicking)
                fireEvent = true;


            return fireEvent;


        }
    }

}
