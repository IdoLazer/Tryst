using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyJoyController : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool getTrailPressed_Player1(){
        return Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.Joystick1Button0);
    }
    public static bool getTrailPressed_Player2(){
        return Input.GetKey(KeyCode.M) || Input.GetKey(KeyCode.Joystick2Button0);
    }
    public static bool getPulsePressed_Player1(){
        return Input.GetKeyUp(KeyCode.X) || Input.GetKeyUp(KeyCode.Joystick1Button1);    
    }
    public static bool getPulsePressed_Player2(){
         return Input.GetKeyUp(KeyCode.N) || Input.GetKeyUp(KeyCode.Joystick2Button1);  
        
    }
    public static float getXAxis_Player1(){
        return Input.GetAxis("Player1X");
    }
    public static float getXAxis_Player2(){
        return Input.GetAxis("Player2X");
    }
    public static float getYAxis_player1(){
        return Input.GetAxis("Player1Y");
    }
    public static float getYAxis_Player2(){
        return Input.GetAxis("Player2Y");
    }
}
