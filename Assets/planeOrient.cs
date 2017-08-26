using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeOrient : MonoBehaviour {


    public DD_steer positionMover;
    public float rotationDamp = 2f;
    Vector3 pos;
    Vector3 velocity;
    public float positionFollowDampTime = 0.1f;
    public float minTiltAngle = -30;
    public float maxTiltAngle = 30;
    public float maxLeftRightRollAngle = 45;
    float zAngle;

    // Use this for initialization
    void Start()
    {

    }

    void TiltRollCalculation()
    {
        //if (pos == Vector3.zero)
        //{
        //    transform.localRotation = Quaternion.identity;
        //}

        if (pos != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(pos);

            float xAngle = rotation.eulerAngles.x;
            if (xAngle < (360 + minTiltAngle) && xAngle >= 270)
            {
                xAngle = (360 + minTiltAngle);
            }
            if (xAngle > maxTiltAngle && xAngle <= 90)
            {
                xAngle = maxTiltAngle;
            }
            float angleToDestination = Vector3.Angle(transform.forward, pos);
            Vector3 cross = Vector3.Cross(transform.forward, pos);
            if (cross.y < 0) angleToDestination = -angleToDestination;
            if (angleToDestination > 1)
            {
                zAngle = Mathf.Clamp((360 - angleToDestination), (360 - maxLeftRightRollAngle), 360);
            }
            if (angleToDestination < 1)
            {
                zAngle = Mathf.Clamp(Mathf.Abs(angleToDestination), 0, maxLeftRightRollAngle);
            }

            Quaternion newRotation = Quaternion.Euler(xAngle, rotation.eulerAngles.y, zAngle);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, newRotation, 1f);
        }


    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.SmoothDamp(transform.position, positionMover.transform.position, ref velocity, positionFollowDampTime);
        //if (!positionMover.isActiveAndEnabled)
        //    this.gameObject.SetActive(false);
        pos = positionMover.transform.position - transform.position;
        TiltRollCalculation();

    }
}
