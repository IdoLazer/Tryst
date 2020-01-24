using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyJoyController : MonoBehaviour
{
    // Start is called before the first frame update

    public bool GetPlayer1TrailPress(){
        return Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.Joystick1Button0);
    }
    public bool GetPlayer2TrailPress(){
        return Input.GetKey(KeyCode.M) || Input.GetKey(KeyCode.Joystick2Button0);
    }
    public bool GetPlayer1PulsePress(){
        return Input.GetKey(KeyCode.X) || Input.GetKeyUp(KeyCode.Joystick1Button1);    
    }
    public bool GetPlayer2PulsePress(){
         return Input.GetKey(KeyCode.N) || Input.GetKeyUp(KeyCode.Joystick2Button1);  
        
    }
}
