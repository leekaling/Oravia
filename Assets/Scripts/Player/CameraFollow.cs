using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region camera_variables
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private Vector3 currentVelocity = Vector3.zero;
    #endregion

    #region Unity_functions
    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
    #endregion

}
