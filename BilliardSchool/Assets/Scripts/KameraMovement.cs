using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraMovement : MonoBehaviour
{
    [SerializeField] public float distanceFromTarget = 5.0f;
    [SerializeField] private Transform whiteBall;   //Braucht den weissen Ball zur Positionierung
    [SerializeField] private Rigidbody whiteBall_rb; //Den Ball muss man einen Impuls geben können
    [SerializeField] private Transform poolTable;   //Den Tisch für Vogelperspektive
    private Vector3 nachVorne;
    private Vector3 nachVorne_richtung;
    [SerializeField] private float smoothenessTime = 0.2f;
    [SerializeField] private float forwardOnCue = 20f;
    private float rotationX = 0f;
    private float rotationY = 0f;
    public float sensivity = 3f;
    private Vector3 smoothVelocity = Vector3.zero;
    private Vector3 currentRotation;
    private int mode = 0;       //Diese Variable sagt, was genau die Kamera macht. 0 ist z.B. Vogelperspektive, 1 wäre die Kamera dann auf der Kugel fixiert.
    
    void CameraAsBirdsEye(){

        transform.localEulerAngles = new Vector3(90, 0, 90);
        transform.localPosition = poolTable.localPosition + new Vector3(0,80,0);

    }
    void CameraOnBall(){

        //Input.mouseScrollDelta.y ------> Wenn der Wert 1: Mausrad nach vorne. Bei -1 nach hinten und 0 ist default
        switch(Input.mouseScrollDelta.y){
            case 1:
                if(distanceFromTarget > 5){
                    distanceFromTarget--;
                }
                break;
            case -1:
                if(distanceFromTarget < 25){
                    distanceFromTarget++;
                }
                break;
            default:
                break;
        }

        //--------------------------------------------------------------------
        // 1. Den Input der Maus Einlesen und die Kamera smooth machen. Der Kamerarichtungsvektor ist also mit dem Vektor "currentRotation" erreichbar

        rotationX += Input.GetAxis("Mouse X") * sensivity;
        
        //Diese If sorgt dafür, dass man nicht auf der Y achse überrotiert.
        if(rotationY <= 75){
            if(rotationY < -15){
                rotationY = -15;
            }
            else{
                rotationY -= Input.GetAxis("Mouse Y") * sensivity;      //Hier -= da bei += sonst invertiert ist aus irgend einem Grund?
            }
        }
        else{
            rotationY = 75;
        }

        //  Die Kamera smooth machen
        Vector3 nextRotation = new Vector3 (rotationY, rotationX, 0);
        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothenessTime);

        //      Siehe "kameraPositioning.png"
        //      1.
        Transform transform_copy = transform;
        transform_copy.localEulerAngles = currentRotation;
        nachVorne = whiteBall.position + transform_copy.forward * forwardOnCue;
        //      2.
        transform.localEulerAngles = currentRotation + new Vector3(15, 0, 0);
        transform.position = nachVorne - (transform.forward) * distanceFromTarget;
        
        if(Input.GetKey("b")){

            whiteBall_rb.velocity = (new Vector3(transform.forward.x, 0, transform.forward.z)) * 50;    //Die Vector Wombo Combo löscht den Push auf der Y-Achse. Sonst könnte man die Kugel in den Tisch pushen, da wir mit der Kamera auch von oben auf die Kugel schauen können.
            
        }

    }

    void Update()
    {
        if(Input.GetKey("a")){
            if(mode == 0){
                mode = 1;
            }
            else{
                mode = 0;
            }
        }
        switch (mode){
            case 0:
                CameraAsBirdsEye();
                break;
            case 1:
                CameraOnBall();
                break;
            default:
                break;
        }
    }
}
