using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstPersonControl : MonoBehaviour
{
    public float mouseSenX = 250f;
    public float mouseSenY = 250f;
    public float rotatSpeen = 5;
    public float fadeBoostSpeed = 1;
    public float fadeBoostSpeedWhenNotMoving = 10;
    public float walkSpeed;
    public float maxWalkSpeed = 10;
    private float initialWalkSpeed;
    private bool hasWon;
    private Vector3 winTarget;
    private float winTime;
    private Vector3 winPos;
    private Quaternion winRotation;

    [Range(0f, 1f)]
    public float controllerSensitivityY = 0.2f;
    [Range(0f, 1f)]
    public float controllerSensitivityX = 0.2f;

    Transform cameraT;
    float verLookRotation;

    Vector3 moveAmount;
    Vector3 smoothMoveVel;
    ShaderScript shaderScript;
    SoundManger soundManger;

    //needed to check if the player is dead
    private GameManager Gm;

    void Start()
    {
        initialWalkSpeed = walkSpeed;
        shaderScript = GetComponent<ShaderScript>();
        soundManger = GetComponent<SoundManger>();
        Gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        FadeBoostSpeed();
        if (hasWon)
        {
            MoveToPos(winTarget);
            return;
        }

        // ================== Player1 =========================

        if (tag == "Player1")
        {
            HandlePlayerOne();
        }

        // ================== Player2 =========================

        if (tag == "Player2")
        {
            HandlePlayerTwo();
        }
    }

    private void MoveToPos(Vector3 winTarget)
    {
        // distance between target and the actual rotating object
        Vector3 D = winTarget - transform.position;


        // calculate the Quaternion for the rotation
        Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(D), Time.deltaTime);

        transform.Rotate(Vector3.up * Time.deltaTime);
    }

    private void HandlePlayerTwo()
    {
        if (!Gm.getIsPlayer2Dead())
        {
            //---- Player 2 Rotating ---
            float axis = KeyJoyController.getXAxis_Player2();
            if (Mathf.Abs(axis) <= controllerSensitivityX)
            {
                axis = 0f;
                shaderScript.StopRotating();
            }
            else
            {
                shaderScript.StartRotating((int)Mathf.Sign(axis));
            }
            //pitch sound
            if (KeyJoyController.getTrailPressed_Player2())
            {
                soundManger.PitchSound("trail", axis);
            }
            else
            {
                soundManger.PitchSound("walk", axis);
            }

            transform.Rotate(rotatSpeen * Vector3.up * axis);

            //---- Player 2 Forward/Backward ---
            axis = KeyJoyController.getYAxis_Player2();
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
                        if (!KeyJoyController.getTrailPressed_Player2())
                        {
                            soundManger.PlaySound("walk");
                        }

                    }
                    else
                    {
                        shaderScript.StopMoving();
                        if (!KeyJoyController.getTrailPressed_Player2())
                        {
                            soundManger.StopPlaying();
                        }
                        if (walkSpeed > initialWalkSpeed)
                        {
                            walkSpeed -= fadeBoostSpeedWhenNotMoving;
                        }
                        else
                        {
                            walkSpeed = initialWalkSpeed;
                        }
                    }
                }
            }
        }

        // ============ Player is dead ============

        else
        {
            moveAmount = new Vector3(0, 0, 0);
            shaderScript.StopMoving();
            soundManger.StopPlaying();

        }
    }

    public void MoveTowards(Vector3 meetingPoint)
    {
        hasWon = true;
        walkSpeed = maxWalkSpeed;
        winTime = Time.time;
        winTarget = meetingPoint;
        winPos = transform.position;
        winRotation = transform.rotation;
    }

    private void HandlePlayerOne()
    {
        if (!Gm.getIsPlayer1Dead())
        {
            //---- Player 1 Rotating ---
            float axis = KeyJoyController.getXAxis_Player1();

            if (Mathf.Abs(axis) <= controllerSensitivityX)
            {
                axis = 0f;
                shaderScript.StopRotating();
            }
            else
            {
                shaderScript.StartRotating((int)Mathf.Sign(axis));
            }

            //pitch sound
            if (KeyJoyController.getTrailPressed_Player1())
            {
                soundManger.PitchSound("trail", axis);
            }
            else
            {
                soundManger.PitchSound("walk", axis);
            }

            transform.Rotate(rotatSpeen * Vector3.up * axis);

            //---- Player 1 Forward/Backward ---
            axis = KeyJoyController.getYAxis_player1();

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
                        if (!KeyJoyController.getTrailPressed_Player1())
                        {
                            soundManger.PlaySound("walk");
                        }

                    }
                    else
                    {
                        shaderScript.StopMoving();
                        if (!KeyJoyController.getTrailPressed_Player1())
                        {
                            soundManger.StopPlaying();
                        }
                        if (walkSpeed > initialWalkSpeed)
                        {
                            walkSpeed -= fadeBoostSpeedWhenNotMoving;
                        }
                        else
                        {
                            walkSpeed = initialWalkSpeed;
                        }
                    }
                }
            }
        }
        else
        {
            moveAmount = new Vector3(0, 0, 0);
            shaderScript.StopMoving();
            walkSpeed = initialWalkSpeed;

        }
    }

    private void FadeBoostSpeed()
    {
        // ============ Fade Boost Speed =========
        walkSpeed = walkSpeed > maxWalkSpeed ? maxWalkSpeed : walkSpeed;

        if (walkSpeed > initialWalkSpeed)
        {
            walkSpeed -= fadeBoostSpeed / 100;
        }
        else
        {
            walkSpeed = initialWalkSpeed;
        }
    }

    void FixedUpdate()
    {
        //Player Movment
        if (hasWon) return;
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);

    }

    public float getInitialWalkSpeed()
    {
        return initialWalkSpeed;
    }
}