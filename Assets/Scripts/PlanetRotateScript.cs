using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotateScript : MonoBehaviour
{
public float rotateSpeed;
    // Update is called once per frame
 public void rotatePlanet(GameObject player1){
    float sensetiviyX = player1.GetComponent<firstPersonControl>().controllerSensitivityX;
    float xAxis = tag == "MenuPlayer1" ? KeyJoyController.getXAxis_Player1() : KeyJoyController.getXAxis_Player2();
    if (Mathf.Abs(xAxis) <= sensetiviyX)
    {
        xAxis = 0f;
    }
    else
    {
        transform.Rotate(Time.deltaTime * rotateSpeed * xAxis, 0, 0 );
    }
 }
}
