using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraMovement : MonoBehaviour
{
    float rotationX = 0f;
    float rotationY = 0f;

    public float sensivity = 15f;
    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * sensivity;

        if(rotationY > 90){
            rotationY = 90;
        }
        else{
            if(rotationY < -90){
                rotationY = -90;
            }
            else{
               rotationY += Input.GetAxis("Mouse Y") * sensivity; 
            }
        }

        transform.localEulerAngles = new Vector3(rotationY*(-1), rotationX, 0);

        Debug.Log("RotationX: " + rotationX);
        Debug.Log("RotationY: " + rotationY);

    }
}
