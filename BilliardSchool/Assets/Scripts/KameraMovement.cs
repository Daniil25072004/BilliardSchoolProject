using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraMovement : MonoBehaviour
{
    [SerializeField] public float distanceFromTarget = 5.0f;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothenessTime = 0.2f;
    private float rotationX = 0f;
    public float sensivity = 3f;
    private Vector3 smoothVelocity = Vector3.zero;
    private Vector3 currentRotation;

    //

    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * sensivity;

        Debug.Log("RotationX: " + rotationX);

        Vector3 nextRotation = new Vector3 (15, rotationX, 0);
        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothenessTime);

        transform.localEulerAngles = currentRotation;
        transform.position = target.position - transform.forward * distanceFromTarget;          //Dieser Command Hält die Kamera an der Kugel fest (Die Kamera wird je nachdem in welche Richtung sie Zeigt um die Kugel herumteleportiert).

    }
}
