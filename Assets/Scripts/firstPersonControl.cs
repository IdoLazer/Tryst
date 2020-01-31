using System.Collections;
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
    SoundManger soundManger;

    //needed to check if the player is dead
    private GameManager Gm;

    void Start()
    {
        shaderScript = GetComponent<ShaderScript>();
        soundManger = GetComponent<SoundManger>();
        Gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        // ================== Player1 =========================

        if (tag == "Player1")
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
                            soundManger.PlaySound("walk");
                        }
                        else
                        {
                            shaderScript.StopMoving();
                           soundManger.StopPlaying();
                        }
                    }
                }
            }
            else
            {
                moveAmount = new Vector3(0, 0, 0);
                shaderScript.StopMoving();

            }

        }

        // ================== Player2 =========================

        if (tag == "Player2")
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

                transform.Rotate(rotatSpeen * Vector3.up * axis);

                //---- Player 2 Forward/Backward ---
                axis = KeyJoyController.getYAxis_Player2();
                if (axis <= 1)
                {
                    Vector3 moveDir = new Vector3(0, 0, axis).normalized;
                    Vector3 targetMoveAmount = moveDir * walkSpeed;
                    moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVel, .15f);

                    if (Mathf.Abs(axis) <= controllerSensitivityY)
                    {
                        axis = 0f;
                    }

                    if (shaderScript != null)
                    {
                        if (Mathf.Abs(axis) > Mathf.Epsilon)
                        {
                            shaderScript.StartMoving();
                            soundManger.PlaySound("walk");
                        }
                        else
                        {
                            shaderScript.StopMoving();
                            soundManger.StopPlaying();
                        }
                    }
                }
            }

            // ============ Player is dead ============
            else
            {
                moveAmount = new Vector3(0, 0, 0);
                shaderScript.StopMoving();

            }

        }
    }


    void FixedUpdate()
    {
        //Player Movment
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);

    }
}