using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstPersonControl : MonoBehaviour
{
    public float mouseSenX = 250f;
    public float mouseSenY = 250f;
    public float rotatSpeen = 5;
    public float walkSpeed;
    Transform cameraT;
    float verLookRotation;

    Vector3 moveAmount;
    Vector3 smoothMoveVel;

    void Start()
    {
    }

    void Update()
    {
        if (tag == "Player1")
        {
            transform.Rotate(rotatSpeen * Vector3.up * Input.GetAxis("Player1X"));
            // if we want the camera to move
            //verLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSenY;
            //verLookRotation = Mathf.Clamp(verLookRotation, -30, 30);
            //cameraT.localEulerAngles = Vector3.left * verLookRotation;
            float axis = Input.GetAxis("Player1Y");
            if (Mathf.Abs(axis) <= 0.1f)
            {
                axis = 0f;
            }
            if (axis <= 1) 
            { 
                Vector3 moveDir = new Vector3(0, 0, -axis).normalized;
                Vector3 targetMoveAmount = moveDir * walkSpeed;
                moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVel, .15f);
            }

        }
        if (tag == "Player2")
        {
            transform.Rotate(rotatSpeen * Vector3.up * Input.GetAxis("Player2X"));
            // if we want the camera to move
            //verLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSenY;
            //verLookRotation = Mathf.Clamp(verLookRotation, -30, 30);
            //cameraT.localEulerAngles = Vector3.left * verLookRotation;
            float axis = Input.GetAxis("Player2Y");
            if (Mathf.Abs(axis) <= 0.1f)
            {
                axis = 0f;
            }

            if (axis <= 1)
            {
                Vector3 moveDir = new Vector3(0, 0, axis).normalized;
                Vector3 targetMoveAmount = moveDir * walkSpeed;
                moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVel, .15f);
            }

        }


    }
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveAmount)*Time.fixedDeltaTime);
    }
}
