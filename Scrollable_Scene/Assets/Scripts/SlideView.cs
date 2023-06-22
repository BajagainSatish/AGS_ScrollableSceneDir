using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideView : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Transform orientation;//orientation of camera
    [SerializeField] private float sensX, sensY;//sensitivity values for rotation along x and y axes
    [SerializeField] private float swipeRange;//minimum threshold distance to trigger rotation

    private Vector2 startTouchPosition, currentTouchPosition;
    private float xRotation, yRotation;

    private void Update()
    {
        SwipeInputsLR();
    }
    public void SwipeInputsLR()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            //co-ordinates are (0,0) at bottom left, (1920,1080) at top right
            //re-adjust values to determine at which areas you don't want camera view to slide, eg. 530 and 450 are just arbitrary values for now
            if (touch.position.x > 530 || touch.position.y > 450)//example: in this case, we implemented no swipe detection at bottom left corner of screen (maybe joystick implementation later on)
            {
                float pointer_x = Input.touches[0].deltaPosition.x * Time.deltaTime * sensX;
                float pointer_y = Input.touches[0].deltaPosition.y * Time.deltaTime * sensY;

                if (touch.phase == TouchPhase.Began)
                {
                    startTouchPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    currentTouchPosition = touch.position;
                    Vector2 Distance = currentTouchPosition - startTouchPosition;
                        if (Distance.x < -swipeRange)//left swipe (swipe distance in x is negative)
                    {
                            yRotation += pointer_x;//rotate the camera to the left
                        }
                        else if (Distance.x > swipeRange)//right swipe
                    {
                            yRotation += pointer_x;//rotate the camera to the right
                    }
                        if (Distance.y > swipeRange)//up swipe
                    {
                            xRotation -= pointer_y;//rotate the camera upwards
                    }
                        else if (Distance.y < -swipeRange)//down swipe
                    {
                            xRotation -= pointer_y;//rotate the camera downwards
                    }
                }
            }
        }
        xRotation = Mathf.Clamp(xRotation, -20f, 20f);//restrict value within specified range, -45 and 45 are arbitrary
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
