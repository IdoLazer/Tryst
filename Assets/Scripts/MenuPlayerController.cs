using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerController : MonoBehaviour
{
    public float walkSpeed = 10;

    [Range(0f, 1f)]
    public float controllerSensitivityY = 0.2f;
    [Range(0f, 1f)]
    public float controllerSensitivityX = 0.2f;
    public bool meet = false;
    private Vector3 startPosition;

    Vector2 moveAmount;
    void Start() {
        meet = false;  
        startPosition = transform.position; 
    }
    // Update is called once per frame
    void Update()
    {

        float xAxis = tag == "MenuPlayer1" ? KeyJoyController.getXAxis_Player1() : KeyJoyController.getXAxis_Player2();
        float yAxis = tag == "MenuPlayer1" ? -KeyJoyController.getYAxis_player1() : KeyJoyController.getYAxis_Player2();

        //---- Player Rotating ---
        if (Mathf.Abs(xAxis) <= controllerSensitivityX)
        {
            xAxis = 0f;
        }
        else
        {
            transform.Translate(Time.deltaTime * walkSpeed * xAxis, 0, 0 );
        }

        //---- Player Forward/Backward ---

        if (Mathf.Abs(yAxis) <= controllerSensitivityY)
        {
            yAxis = 0f;
        }
        else
        {
            transform.Translate(0 , Time.deltaTime * walkSpeed * yAxis, 0 );
        }


    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "MenuPlayer1" || other.gameObject.tag == "MenuPlayer1" ){
            meet = true;
        }
    }
    public void reset(){
        meet = false;
        transform.position = startPosition;
    }
}
