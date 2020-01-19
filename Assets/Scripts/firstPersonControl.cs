﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstPersonControl : MonoBehaviour
{
    public float mouseSenX = 250f;
    public float mouseSenY = 250f;
    public float rotatSpeen = 5;
    public float walkSpeed;

    [Range(0f, 1f)]
    public float controllerSensitivityY = 0.2f;
    [Range(0f, 1f)]
    public float controllerSensitivityX = 0.2f;

    Transform cameraT;
    float verLookRotation;

    Vector3 moveAmount;
    Vector3 smoothMoveVel;
    ShaderScript shaderScript;

    void Start()
    {
        shaderScript = GetComponent<ShaderScript>();
    }

    void Update()
    {
        if (tag == "Player1")
        {
            float axis = Input.GetAxis("Player1X");

            if (Mathf.Abs(axis) <= controllerSensitivityX)
            {
                axis = 0f;
            }

            transform.Rotate(rotatSpeen * Vector3.up * axis);
            // if we want the camera to move
            //verLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSenY;
            //verLookRotation = Mathf.Clamp(verLookRotation, -30, 30);
            //cameraT.localEulerAngles = Vector3.left * verLookRotation;
            axis = Input.GetAxis("Player1Y");
            
            if (Mathf.Abs(axis) <= controllerSensitivityY)
            {
                axis = 0f;
            }

            if (axis <= 1)
            {
                Vector3 moveDir = new Vector3(0, 0, -axis).normalized;
                Vector3 targetMoveAmount = moveDir * walkSpeed;
                moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVel, .15f);
                if (shaderScript != null)
                {
                    if (Mathf.Abs(axis) > Mathf.Epsilon)
                    {
                        shaderScript.StartMoving();
                    }
                    else
                    {
                        shaderScript.StopMoving();
                    }
                }
            }

        }
        if (tag == "Player2")
        {
            float axis = Input.GetAxis("Player2X");
            if (Mathf.Abs(axis) <= controllerSensitivityX)
            {
                axis = 0f;
            }

            transform.Rotate(rotatSpeen * Vector3.up *axis);
            // if we want the camera to move
            //verLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSenY;
            //verLookRotation = Mathf.Clamp(verLookRotation, -30, 30);
            //cameraT.localEulerAngles = Vector3.left * verLookRotation;
            axis = Input.GetAxis("Player2Y");

            if (Mathf.Abs(axis) <= controllerSensitivityY)
            {
                axis = 0f;
            }

            if (axis <= 1)
            {
                Vector3 moveDir = new Vector3(0, 0, axis).normalized;
                Vector3 targetMoveAmount = moveDir * walkSpeed;
                moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVel, .15f);
                if (shaderScript != null)
                {
                    if (Mathf.Abs(axis) > Mathf.Epsilon)
                    {
                        shaderScript.StartMoving();
                    }
                    else
                    {
                        shaderScript.StopMoving();
                    }
                }
            }

        }


    }
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }
}