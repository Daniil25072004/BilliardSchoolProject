using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraMovement : MonoBehaviour
{
    private float rotationX = 0f;
    private float rotationY = 0f;
    public float sensivity = 15f;
    void Update()
    {
                rotationX += Input.GetAxis("Mouse X") * sensivity;

        rotationY += Input.GetAxis("Mouse Y") * sensivity; 
        
        //Sodass man nicht unendlich nach unten sehen kann
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
        if(rotationY < -90){
            rotationY = -90;
        }

        transform.localEulerAngles = new Vector3(rotationY*(-1), rotationX, 0);

        Debug.Log("RotationX: " + rotationX);
        Debug.Log("RotationY: " + rotationY);

        transform.localEulerAngles = new Vector3(rotationY*(-1), rotationX, 0);     //X,Y,Z Keine Ahnung warum Dann zuerst im Vector die YRotation kommt?
                                                                                    //Wenn der erste Parameter rotationX ist, wird die Rotation auf der X-Achse
                                                                                    //invertiert
        }
    }
}
