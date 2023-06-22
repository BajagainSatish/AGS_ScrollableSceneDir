using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] float zoomSpeed = 10f;
    [SerializeField] float fieldOfViewMax = 50;
    [SerializeField] float fieldOfViewMin = 10;

    private float targetFieldOfView = 50;
    private float touchesPrevPosDifference, touchesCurPosDifference;

    private Vector2 firstTouchPrevPos, secondTouchPrevPos;

    private void Update()
    {
        HandleCameraZoom();
    }
    private void HandleCameraZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            if (touchesPrevPosDifference > touchesCurPosDifference)
            {
                targetFieldOfView += 5;
            }
            if (touchesPrevPosDifference < touchesCurPosDifference)
            {
                targetFieldOfView -= 5;
            }
        }
        targetFieldOfView = Mathf.Clamp(targetFieldOfView, fieldOfViewMin, fieldOfViewMax);
        cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(cinemachineVirtualCamera.m_Lens.FieldOfView, targetFieldOfView, Time.deltaTime * zoomSpeed);
    }
}
