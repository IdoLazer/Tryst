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

    //needed to check if the player is dead
    private GameManager Gm;

    void Start()
    {
        shaderScript = GetComponent<ShaderScript>();
        Gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (tag == "Player1" && !(Gm.playerOneLose))
        {
            Debug.Log(Gm.playerOneLose);
            float axis = Input.GetAxis("Player1X");

            if (Mathf.Abs(axis) <= controllerSensitivityX)
            {
                axis = 0f;
            }

            transform.Rotate(rotatSpeen * Vector3.up * axis);
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
        if (tag == "Player2" && !(Gm.playerTwoLose))
        {
            float axis = Input.GetAxis("Player2X");
            if (Mathf.Abs(axis) <= controllerSensitivityX)
            {
                axis = 0f;
            }

            transform.Rotate(rotatSpeen * Vector3.up *axis);
            axis = Input.GetAxis("Player2Y");

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

        playWhenMove("Player2X");
        playWhenMove("Player1X");
        playWhenMove("Player2Y");
        playWhenMove("Player1Y");
    }


    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);

    }

    private void playWhenMove(string axsisName)
    {
        if (Input.GetButtonDown(axsisName))
        {

            SoundManger.PlaySound("walk");
        }

        if (Input.GetButtonUp(axsisName))
        {

            SoundManger.StopPlaying();
        }

    }
}
